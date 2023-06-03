using Microsoft.CodeAnalysis.FlowAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
                yield return service.GetInstance<T>();
            }
        }

        private record Service
        {
            public Type Type { get; }

            private object _instance = null;

            public Service(Type type)
            {
                Type = type;
            }

            public T GetInstance<T>()
            {
                if (_instance == null)
                {
                    _instance = Activator.CreateInstance(Type);
                }

                return (T)_instance;
            }
        }
    }
}
