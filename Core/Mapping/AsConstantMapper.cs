using System;
using System.Collections.Concurrent;

namespace Core
{
    internal class AsConstantMapper : IMapper
    {
        private readonly Action<Type, Func<Func<Type, object>, object>> _addMapping;
        private readonly ConcurrentDictionary<Type, object> _cache = new ConcurrentDictionary<Type, object>();

        internal AsConstantMapper(Action<Type, Func<Func<Type, object>, object>> addMapping)
        {
            _addMapping = addMapping;
        }

        public IMapper Map<TRequested, TActual>()
        {
            var tRequested = typeof(TRequested);
            var tActual = typeof(TActual);
            _addMapping(
                tRequested, 
                creator => GetOrCreateInstance(tRequested, tActual, creator)
            );

            return this;
        }

        private object GetOrCreateInstance(Type tRequested, Type tActual, Func<Type, object> creator)
        {
            return _cache.GetOrAdd(tRequested, typeToIgnore => creator(tActual));
        }
    }
}
