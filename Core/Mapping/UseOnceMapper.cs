using System;

namespace Core
{
    internal class UseOnceMapper : IMapper
    {
        private readonly Action<Type, Func<Func<Type, object>, object>> _addMapping;

        internal UseOnceMapper(Action<Type, Func<Func<Type, object>, object>> addMapping)
        {
            _addMapping = addMapping;
        }

        public IMapper Map<TRequested, TActual>()
        {
            _addMapping(
                typeof(TRequested), 
                creator => creator(typeof(TActual))
            );

            return this;
        }
    }
}
