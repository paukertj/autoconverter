using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
                yield return (T)GetServiceInstance(service.Type, service);
                //var constructors = service.Type.GetConstructors();

                //if (constructors.Length > 1)
                //{
                //    throw new Exception($"Unable to build service '{service.Type} : {typeof(T)}' because there are '{constructors.Length}' constructors but only '1' expected");
                //}

                //var paramteres = constructors
                //    .First()
                //    .GetParameters()
                //    .Select;

                //foreach (var parameter in paramteres)
                //{
                //    parameter.ParameterType.
                //}

                //yield return service.GetInstance<T>();
            }
        }

        private object GetServiceInstance(Type type, Service service)
        {
            if (service == null)
            {
                if (_container.TryGetValue(type, out var services) == false || services?.Any() != true)
                {
                    throw new Exception($"Unable to find serivce '{type}'");
                }

                service = services.First();
            }

            var constructors = service.Type.GetConstructors();

            if (constructors.Length > 1)
            {
                throw new Exception($"Unable to build service '{service.Type} : {type}' because there are '{constructors.Length}' constructors but only '1' expected");
            }

            var paramters = constructors
                .First()
                .GetParameters();

            if (paramters.Length <= 0)
            {
                return service.GetInstance();
            }

            var paramterInstances = paramters
                .Select(p => GetServiceInstance(p.ParameterType, null))
                .ToArray();

            return service.GetInstance(paramterInstances);
        }

        //private IEnumerable<object> GetServices(Type type)
        //{
        //    if (_container.TryGetValue(type, out var services) == false)
        //    {
        //        throw new Exception($"Unable to find serivce '{type}'");
        //    }

        //    foreach (var service in services)
        //    {
        //        var constructors = service.Type.GetConstructors();

        //        if (constructors.Length > 1)
        //        {
        //            throw new Exception($"Unable to build service '{service.Type} : {type}' because there are '{constructors.Length}' constructors but only '1' expected");
        //        }

        //        var paramteres = constructors
        //            .First()
        //            .GetParameters()
        //            .Select(p => GetServices(p.ParameterType));

        //        yield return service.GetInstance(paramteres);
        //    }
        //}

        private record Service
        {
            public Type Type { get; }

            private object _instance = null;

            public Service(Type type)
            {
                Type = type;
            }

            public T GetInstance<T>(params object[] args)
            {
                return (T)GetInstance(args);
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
