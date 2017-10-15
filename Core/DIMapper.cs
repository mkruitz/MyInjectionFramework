using System;
using System.Collections.Generic;

namespace Core
{
    public class DIMapper
    {
        private readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();

        public void Map<TRequested, TActual>()
        {
            var tRequested = typeof(TRequested);

            if (_mappings.ContainsKey(tRequested))
            {
                throw new DoubleMappingException(tRequested);
            }

            _mappings.Add(tRequested, typeof(TActual));
        }

        internal Type MapType(Type t)
        {
            if (_mappings.ContainsKey(t))
            {
                return _mappings[t];
            }

            return t.IsInterface
                ? null
                : t;
        }
    }
}
