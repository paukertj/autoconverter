using Microsoft.CodeAnalysis;
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

        private Dictionary<ServiceKey, List<ServiceBase>> _container = new Dictionary<ServiceKey, List<ServiceBase>>();

        public BuilderService()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _types = _assembly.GetTypes();

            RegisterSelf();
        }

        private void RegisterSelf()
        {
            var services = new List<ServiceBase>
            {
                new SingletonService(this, null, typeof(IBuilderService))
            };

            _container.Add(ServiceKey.Create<IBuilderService>(), services);
        }

        public IBuilderService AddSingletons<T>(params object[] args)
        {
            AddServicesInternal<T>(t => new SingletonService(t, typeof(T).GetGenericArguments(), args));

            return this;
        }

        public IBuilderService AddTransients<T>(params object[] args)
        {
            AddServicesInternal<T>(t => new TransientService(t, typeof(T).GetGenericArguments(), args));

            return this;
        }

        private void AddServicesInternal<T>(Func<Type, ServiceBase> factory)
        {
            var services = _types
                 .Where(IsAssignableFrom<T>)
                 .Select(factory)
                 .ToList();

            List<ServiceBase> existingServices;

            var serviceKey = ServiceKey.Create<T>();

            if (_container.TryGetValue(ServiceKey.Create<T>(), out existingServices) == false)
            {
                existingServices = new List<ServiceBase>(services.Count);
                _container.Add(ServiceKey.Create<T>(), existingServices);
            }

            var registeredAndNewServices = services
                .GroupJoin(_container.Values.SelectMany(s => s), n => new ServiceKey(n.Type, n.GenericArguments), r => new ServiceKey(r.Type, r.GenericArguments), (n, r) => new
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
        }

        private static bool IsAssignableFrom<T>(Type impelemntation)
        {
            if (impelemntation.IsClass == false)
            {
                return false;
            }

            if (typeof(T).IsAssignableFrom(impelemntation))
            {
                return true;
            }

            if (typeof(T).IsGenericType == false)
            {
                return false;
            }

            return impelemntation
                .GetInterfaces()
                .Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(T).GetGenericTypeDefinition());
        }

        public T GetService<T>()
        {
            return GetServices<T>().FirstOrDefault();
        }

        public IEnumerable<T> GetServices<T>()
        {
            if (_container.TryGetValue(ServiceKey.Create<T>(), out var services) == false)
            {
                throw new Exception($"Unable to find serivce '{typeof(T)}'");
            }

            foreach (var service in services)
            {
                yield return (T)GetServiceInstance(service);
            }
        }

        private object GetServiceInstance(ServiceBase service)
        {
            var constructors = service.Type.GetConstructors();

            if (constructors.Length > 1)
            {
                throw new Exception($"Unable to build service '{service.Type}' because there are '{constructors.Length}' constructors but only '1' expected");
            }

            if (constructors.Length == 0)
            {
                return service.GetInstance();
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
                // This is still naive but should be no coplex references
                throw new Exception($"Unable to build service '{service.Type}' because there are circular references '{string.Join(", ", circularReferences)}'");
            }

            if (paramters.Length <= 0)
            {
                return service.GetInstance();
            }

            var typesToExclude = service.Args?
                .Select(a => a.GetType()) ?? Enumerable.Empty<Type>();

            var paramterInstances = paramters
                .Where(p => typesToExclude.Contains(p.ParameterType) == false)
                .Select(p => ActivateFirstServiceByType(p.ParameterType))
                .ToArray();

            return service.GetInstance(paramterInstances);
        }

        private object ActivateFirstServiceByType(Type type)
        {
            var key = new ServiceKey(type, type.GetGenericArguments());

            if (_container.TryGetValue(key, out var services) == false || services?.Any() != true)
            {
                throw new Exception($"Unable to find serivce '{type}'");
            }

            var service = services.First();

            return GetServiceInstance(service);
        }

        private class SingletonService : ServiceBase
        {
            private object _instance = null;

            public SingletonService(Type type, Type[] genericArguments, params object[] args)
                : base(type, genericArguments, args)
            {

            }

            public SingletonService(object instance, Type[] genericArguments, Type type)
                : base(type, genericArguments)
            {
                _instance = instance;
            }

            public override object GetInstance(params object[] args)
            {
                if (_instance == null)
                {
                    _instance = base.GetInstance(args);
                }

                return _instance;
            }
        }

        private class TransientService : ServiceBase
        {
            public TransientService(Type type, Type[] genericArguments, params object[] args)
                : base(type, genericArguments, args)
            {

            }
        }

        private abstract class ServiceBase 
        {
            public Type Type { get; }

            public IReadOnlyList<Type> GenericArguments { get; }

            public IReadOnlyList<object> Args { get; }

            protected ServiceBase(Type type, Type[] genericArguments, params object[] args)
            {
                Type = type;
                GenericArguments = genericArguments;
                Args = args;
            }

            public virtual object GetInstance(params object[] args)
            {
                if (Args?.Any() == true)
                {
                    var a = Args.ToList();
                    a.AddRange(args);

                    args = a.ToArray();
                }

                if (Type.IsGenericType == false)
                {
                    return Activator.CreateInstance(Type, args);
                }

                var genericArguments = GenericArguments.ToArray();
                var genericType = Type.MakeGenericType(genericArguments);

                return Activator.CreateInstance(genericType, args);
            }
        }

        private class ServiceKey
        {
            public Type Type { get; }

            public IReadOnlyList<Type> GenericArgument { get; }


            public ServiceKey(Type type, IEnumerable<Type> genericArguments)
            {
                Type = type;
                GenericArgument = genericArguments?.ToList();
            }

            public override bool Equals(object obj)
            {
                if (obj is ServiceKey serviceKey == false)
                {
                    return false;
                }

                bool iHaveGeneric = GenericArgument?.Any() == true;
                bool theyHaveGeneric = serviceKey.GenericArgument?.Any() == true;

                if (iHaveGeneric != theyHaveGeneric)
                {
                    return false;
                }

                bool isValidType = Type == serviceKey.Type;

                if (iHaveGeneric == theyHaveGeneric && iHaveGeneric == false)
                {
                    return isValidType;
                }

                int sameGenerics = GenericArgument
                    .Join(serviceKey.GenericArgument, i => i, t => t, (i, t) => i)
                    .Count();

                return 
                    isValidType && 
                    sameGenerics == GenericArgument.Count && 
                    GenericArgument.Count == serviceKey.GenericArgument.Count;
            }

            public override int GetHashCode()
            {
                return 0;
            }

            public static ServiceKey Create<T>()
            {
                return new ServiceKey(typeof(T), typeof(T).GetGenericArguments());
            }
        }
    }
}
