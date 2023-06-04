using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Paukertj.Autoconverter.Generator.Services.Builder
{
    internal sealed class BuilderService : IBuilderService
    {
        private readonly Assembly _assembly;
        private readonly IReadOnlyList<Type> _types;

        private Dictionary<Type, List<Service>> _container = new Dictionary<Type, List<Service>>();

        public BuilderService()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _types = _assembly.GetTypes();
        }

        public void AddServices<T>()
        {
            var services = _types
                 .Where(t => typeof(T).IsAssignableFrom(t) && t.IsClass)
                 .Select(t => new Service(t))
                 .ToList();

            List<Service> existingServices;

            if (_container.TryGetValue(typeof(T), out existingServices) == false)
            {
                existingServices = new List<Service>(services.Count);
            }

            var registeredAndNewServices = services
                .GroupJoin(_container.Values.SelectMany(s => s), n => n.Type, r => r.Type, (n, r) => new
                {
                    New = n,
                    Registered = r?.FirstOrDefault()
                });

            foreach (var registeredAndNewService in registeredAndNewServices)
            {
                if (registeredAndNewService.Registered == null)
                {
                    existingServices.Add(registeredAndNewService.New);
                }
                else
                {
                    existingServices.Add(registeredAndNewService.Registered);
                }
            }

            _container.Add(typeof(T), existingServices);
        }

        public IEnumerable<T> GetServices<T>()
        {
            if (_container.TryGetValue(typeof(T), out var services) == false)
            {
                throw new Exception($"Unable to find serivce '{typeof(T)}'");
            }

            foreach (var service in services)
            {
                yield return (T)GetServiceInstance(service);
            }
        }

        private object GetServiceInstance(Service service)
        {
            var constructors = service.Type.GetConstructors();

            if (constructors.Length > 1)
            {
                throw new Exception($"Unable to build service '{service.Type}' because there are '{constructors.Length}' constructors but only '1' expected");
            }

            var paramters = constructors
                .First()
                .GetParameters();

            var circularReferences = service.Type
                .GetInterfaces()
                .Join(paramters, s => s, p => p.ParameterType, (s, p) => $"{s} - {p}")
                .ToList();

            if (circularReferences.Count > 0)
            {
                throw new Exception($"Unable to build service '{service.Type}' because there are circular references '{string.Join(", ", circularReferences)}'");
            }

            if (paramters.Length <= 0)
            {
                return service.GetInstance();
            }

            var paramterInstances = paramters
                .Select(p => ActivateFirstServiceByType(p.ParameterType))
                .ToArray();

            return service.GetInstance(paramterInstances);
        }

        private object ActivateFirstServiceByType(Type type)
        {
            if (_container.TryGetValue(type, out var services) == false || services?.Any() != true)
            {
                throw new Exception($"Unable to find serivce '{type}'");
            }

            var service = services.First();

            return GetServiceInstance(service);
        }

        private record Service
        {
            public Type Type { get; }

            private object _instance = null;

            public Service(Type type)
            {
                Type = type;
            }

            public object GetInstance(params object[] args)
            {
                if (_instance == null)
                {
                    _instance = Activator.CreateInstance(Type, args);
                }

                return _instance;
            }
        }
    }
}
