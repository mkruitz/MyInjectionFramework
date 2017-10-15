using System;
using System.Collections.Generic;

namespace Core
{
    public class DIMapper
    {
        // Func<Func<Type, object>, object>
        // InstanceGetter<
        //                Constructor<TypeToGetInstance, Instance>, 
        // Instance>
        private readonly Dictionary<Type, Func<Func<Type, object>, object>> _mappings = new Dictionary<Type, Func<Func<Type, object>, object>>();

        public IMapper AsUseOnce()
        {
            return new UseOnceMapper(AddMapping);
        }

        public IMapper AsConstant()
        {
            return new AsConstantMapper(AddMapping);
        }

        private void AddMapping(Type tRequested, Func<Func<Type, object>, object> getter)
        {
            if (_mappings.ContainsKey(tRequested))
            {
                throw new DoubleMappingException(tRequested);
            }

            _mappings.Add(tRequested, getter);
        }

        internal object GetOrCreate(Type t, Func<Type, object> creator)
        {
            if (_mappings.ContainsKey(t))
            {
                return _mappings[t](creator);
            }

            return t.IsInterface
                ? null
                : creator(t);
        }
    }
}
