using System;
using System.Collections.Concurrent;

namespace Core
{
    public class DICache
    {
        private readonly ConcurrentDictionary<Type, object> _cache = new ConcurrentDictionary<Type, object>();

        public object Get(Type type, Func<Type, object> creator)
        {
            return _cache.GetOrAdd(type, creator);
        }
    }
}
