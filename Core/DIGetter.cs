using System;
using System.Collections.Generic;

namespace Core
{
    public class DIGetter
    {
        private readonly DIMapper _mapper;
        private readonly DICache _cache = new DICache();

        public DIGetter(DIMapper mapper)
        {
            _mapper = mapper;
        }

        public T Get<T>() where T : class
        {
            return (T) Get(typeof(T));
        }

        private object Get(Type tRequested)
        {
            var instance = _mapper.GetOrCreate(tRequested, CreateInstance);

            if (instance == null)
            {
                throw new MissingMappingException(tRequested);
            }

            return instance;
        }

        private object CreateInstance(Type tCreating)
        {
            var constructorInfo = tCreating.GetConstructors()[0];

            var args = new List<object>();

            foreach (var parameter in constructorInfo.GetParameters())
            {
                var tRequested = parameter.ParameterType;
                var instance = _mapper.GetOrCreate(tRequested, CreateInstance);
                if (instance == null)
                {
                    throw new MissingDependancyException(tCreating, tRequested);
                }

                args.Add(instance);
            }

            return constructorInfo.Invoke(args.ToArray());
        }
    }
}
