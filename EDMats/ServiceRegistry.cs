using System;
using System.Collections.Generic;
using System.Linq;

namespace EDMats
{
    public class ServiceRegistry
    {
        private readonly ISet<object> _requestedResources = new HashSet<object>();
        private readonly IDictionary<string, Func<object>> _factories = new Dictionary<string, Func<object>>(StringComparer.OrdinalIgnoreCase);
        private readonly IDictionary<Type, Func<object>> _services = new Dictionary<Type, Func<object>>();

        public object this[string key]
        {
            get
            {
                var loggedKey = key?.ToLowerInvariant() ?? throw new ArgumentNullException(nameof(key));
                lock (_requestedResources)
                    if (!_requestedResources.Add(loggedKey))
                        throw new InvalidOperationException("Circular reference detected.");

                try
                {
                    return _factories[key]();
                }
                finally
                {
                    lock (_requestedResources)
                        _requestedResources.Remove(loggedKey);
                }
            }
        }

        public object this[Type service]
        {
            get
            {
                if (service == null)
                    throw new ArgumentNullException(nameof(service));

                lock (_requestedResources)
                    if (!_requestedResources.Add(service))
                        throw new InvalidOperationException("Circular reference detected.");

                try
                {
                    return _services[service]();
                }
                finally
                {
                    lock (_requestedResources)
                        _requestedResources.Remove(service);
                }
            }
        }

        protected void Add(Type service, object implementation)
        {
            _ValidateSingleton(service, implementation);
            _services.Add(service, () => implementation);
        }

        protected void Add(string key, Delegate factory)
        {
            if (factory.Method.GetParameters().Length == 1)
                _factories.Add(key, () => factory.DynamicInvoke(this));
            else
                _factories.Add(key, () => factory.DynamicInvoke());
        }

        protected void Add(string key, Type service, object implementation)
        {
            _ValidateSingleton(service, implementation);
            _factories.Add(key, () => implementation);
            _services.Add(service, () => implementation);
        }

        protected void Add(string key, Type implementation)
        {
            if (implementation == null)
                throw new ArgumentNullException(nameof(implementation));

            var constructors = implementation.GetConstructors();
            if (constructors.Length > 1)
                throw new ArgumentException($"Only one constructor is expected for type {implementation.Name}.");
            var constructor = constructors.Single();
            Add(
                key,
                implementation,
                new Func<ServiceRegistry, object>(
                    serviceRegistry =>
                    {
                        var constructorParameters = constructor.GetParameters().Select(parameter => serviceRegistry[parameter.ParameterType]);
                        return Activator.CreateInstance(implementation, constructorParameters);
                    }
                )
            );
        }

        protected void Add(Type service, Type implementation)
        {
            if (implementation == null)
                throw new ArgumentNullException(nameof(implementation));

            var constructors = implementation.GetConstructors();
            if (constructors.Length > 1)
                throw new ArgumentException($"Only one constructor is expected for type {implementation.Name}.");
            var constructor = constructors.Single();
            Add(
                service,
                implementation,
                new Func<ServiceRegistry, object>(
                    serviceRegistry =>
                    {
                        var constructorParameters = constructor.GetParameters().Select(parameter => serviceRegistry[parameter.ParameterType]);
                        return Activator.CreateInstance(implementation, constructorParameters);
                    }
                )
            );
        }

        protected void Add(Type service, Type implementation, Delegate factory)
        {
            _ValidateFactory(service, implementation, factory);
            if (factory.Method.GetParameters().Length == 1)
                _services.Add(service, () => factory.DynamicInvoke(this));
            else
                _services.Add(service, () => factory.DynamicInvoke());
        }

        protected void Add(string key, Type service, Type implementation, Delegate factory)
        {
            _ValidateFactory(service, implementation, factory);
            if (factory.Method.GetParameters().Length == 1)
            {
                _factories.Add(key, () => factory.DynamicInvoke(this));
                _services.Add(service, () => factory.DynamicInvoke(this));
            }
            else
            {
                _factories.Add(key, () => factory.DynamicInvoke());
                _services.Add(service, () => factory.DynamicInvoke());
            }
        }

        protected void Add<TService, TImplementation>(Func<TImplementation> factory) where TService : TImplementation
        {
            _services.Add(typeof(TService), () => factory());
        }

        protected void Add<TService, TImplementation>(Func<ServiceRegistry, TImplementation> factory) where TService : TImplementation
        {
            _services.Add(typeof(TService), () => factory(this));
        }

        protected void Add<TService, TImplementation>(string key, Func<TImplementation> factory) where TService : TImplementation
        {
            _factories.Add(key, () => factory());
            _services.Add(typeof(TService), () => factory());
        }

        protected void Add<TService, TImplementation>(string key, Func<ServiceRegistry, TImplementation> factory) where TService : TImplementation
        {
            _factories.Add(key, () => factory(this));
            _services.Add(typeof(TService), () => factory(this));
        }

        protected void AddSingleton<TService, TImplementation>(TImplementation implementation) where TService : TImplementation
        {
            _services.Add(typeof(TService), () => implementation);
        }

        protected void AddSingleton<TService, TImplementation>(string key, TImplementation implementation) where TService : TImplementation
        {
            _factories.Add(key, () => implementation);
            _services.Add(typeof(TService), () => implementation);
        }

        protected void AddSingleton<TService, TImplementation>(Func<TImplementation> factory) where TService : TImplementation
        {
            var singletonFactory = new Lazy<TImplementation>(factory);
            _services.Add(typeof(TService), () => singletonFactory.Value);
        }

        protected void AddSingleton<TService, TImplementation>(Func<ServiceRegistry, TImplementation> factory) where TService : TImplementation
        {
            var singletonFactory = new Lazy<TImplementation>(() => factory(this));
            _services.Add(typeof(TService), () => singletonFactory.Value);
        }

        protected void AddSingleton<TService, TImplementation>(string key, Func<TImplementation> factory) where TService : TImplementation
        {
            var singletonFactory = new Lazy<TImplementation>(factory);
            _factories.Add(key, () => singletonFactory.Value);
            _services.Add(typeof(TService), () => singletonFactory.Value);
        }

        protected void AddSingleton<TService, TImplementation>(string key, Func<ServiceRegistry, TImplementation> factory) where TService : TImplementation
        {
            var singletonFactory = new Lazy<TImplementation>(() => factory(this));
            _factories.Add(key, () => singletonFactory.Value);
            _services.Add(typeof(TService), () => singletonFactory.Value);
        }

        private static void _ValidateSingleton(Type service, object implementation)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (implementation == null)
                throw new ArgumentNullException(nameof(implementation));
            if (!service.IsAssignableFrom(implementation.GetType()))
                throw new ArgumentException($"The implementation ({implementation.GetType().Name}) cannot be assigned to the service ({service.Name}).", nameof(implementation));
        }

        private static void _ValidateFactory(Type service, Type implementation, Delegate factory)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (implementation == null)
                throw new ArgumentNullException(nameof(implementation));
            if (!service.IsAssignableFrom(implementation))
                throw new ArgumentException($"The implementation ({implementation.Name}) cannot be assigned to the service ({service.Name}).", nameof(implementation));
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            var factoryParameters = factory.Method.GetParameters();
            if (factoryParameters.Length > 1
                || (factoryParameters.Length == 1 && !factoryParameters[0].ParameterType.IsAssignableFrom(typeof(ServiceRegistry))))
                throw new ArgumentException($"The factory must take at most one parameter which is assignable from {typeof(ServiceRegistry).Name}.", nameof(factory));

            if (!implementation.IsAssignableFrom(factory.Method.ReturnType))
                throw new ArgumentException($"The factory result ({factory.Method.ReturnType.Name}) cannot be assigned to the implementation ({implementation.Name}).", nameof(factory));
        }
    }
}