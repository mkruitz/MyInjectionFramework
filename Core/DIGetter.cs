using System;
using System.Collections.Generic;

namespace Core
{
    public class DIGetter
    {
        private readonly DIMapper _mapper;

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
            var t = _mapper.MapType(tRequested);

            if (t == null)
            {
                throw new MissingMappingException(tRequested);
            }

            return CreateInstance(t);
        }

        private object CreateInstance(Type tCreating)
        {
            var constructorInfo = tCreating.GetConstructors()[0];

            var args = new List<object>();

            foreach (var parameter in constructorInfo.GetParameters())
            {
                var tRequested = parameter.ParameterType;
                var t = _mapper.MapType(tRequested);
                if (t == null)
                {
                    throw new MissingDependancyException(tCreating, tRequested);
                }

                args.Add(CreateInstance(t));
            }

            return constructorInfo.Invoke(args.ToArray());
        }
    }
}
