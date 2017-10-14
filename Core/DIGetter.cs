using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core
{
    public class DIGetter
    {
        private readonly Dictionary<Type, Type> mappings = new Dictionary<Type, Type>();

        public void Map<TRequested, TActual>()
        {
            var tRequested = typeof(TRequested);

            if (mappings.ContainsKey(tRequested))
            {
                throw new DoubleMappingException(tRequested);
            }

            mappings.Add(tRequested, typeof(TActual));
        }

        public T Get<T>() where T : class
        {
            return (T) Get(typeof(T));
        }

        private object Get(Type tRequested)
        {
            var t = MapType(tRequested);

            if (t == null)
            {
                throw new MissingMappingException(tRequested);
            }

            return CreateInstance(t);
        }

        private Type MapType(Type t)
        {
            if (mappings.ContainsKey(t))
            {
                return mappings[t];
            }

            return t.IsInterface
                ? null
                : t;
        }

        private object CreateInstance(Type tCreating)
        {
            var constructorInfo = tCreating.GetConstructors()[0];

            var args = new List<object>();

            foreach (var parameter in constructorInfo.GetParameters())
            {
                var tRequested = parameter.ParameterType;
                var t = MapType(tRequested);
                if (t == null)
                {
                    throw new MissingDependancyException(tCreating, tRequested);
                }

                args.Add(t);
            }

            return constructorInfo.Invoke(args.ToArray());
        }
    }
}
