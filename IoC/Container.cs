namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Container
    {
        Dictionary<Type, Type> map = new Dictionary<Type, Type>(); 

        public ContainerBuilder For<TSource>()
        {
            return For(typeof(TSource));
        }

        public ContainerBuilder For(Type sourceType)
        {
            return new ContainerBuilder(this, sourceType);
        }

        public object Resolve<TSource>()
        {
            return (TSource)Resolve(typeof(TSource));
        }

        public object Resolve(Type sourceType)
        {
            if (map.ContainsKey(sourceType))
            {
                var destinationType = map[sourceType];
                return CreateInstance(destinationType);
            }
            else if (sourceType.IsGenericType // For resolving concrete types BUT with IRepository<Employee> OR IRepository<Customer> etc
                && map.ContainsKey(sourceType.GetGenericTypeDefinition()))
            {
                var destination = map[sourceType.GetGenericTypeDefinition()];
                var closedDestination = destination.MakeGenericType(sourceType.GetGenericArguments());

                return this.CreateInstance(closedDestination);
            }
            else if (!sourceType.IsAbstract) // For resolving concrete types
            {
                return this.CreateInstance(sourceType);
            }
            else
            {
                throw new InvalidOperationException("Could not resolve " + sourceType.FullName);
            }
        }

        private object CreateInstance(Type destinationType)
        {
            // Get constructor with most parameters
            var parameters =
                destinationType.GetConstructors()
                    .OrderByDescending(c => c.GetParameters().Count())
                    .First()
                    .GetParameters()
                    .Select(p => Resolve(p.ParameterType))
                    .ToArray();

            return Activator.CreateInstance(destinationType, parameters);
        }

        // Inner class
        public class ContainerBuilder
        {
            Container container;

            Type source;

            public ContainerBuilder Use<TDestination>()
            {
                return Use(typeof(TDestination));
            }
            
            public ContainerBuilder Use(Type destinationType)
            {
                container.map.Add(source, destinationType);
                return this;
            }

            public ContainerBuilder(Container container, Type sourceType)
            {
                this.container = container;
                this.source = sourceType;
            }
        }
    }
}
