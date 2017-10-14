using System;

namespace Core
{
    public abstract class DIException : Exception { }

    public class MissingMappingException : DIException
    {
        public Type TMissing { get; }

        public MissingMappingException(Type tMissing)
        {
            TMissing = tMissing;
        }
    }

    public class DoubleMappingException : DIException
    {
        public Type TRequested { get; }

        public DoubleMappingException(Type tRequested)
        {
            TRequested = tRequested;
        }
    }

    public class MissingDependancyException : DIException
    {
        public Type TCreating { get; }
        public Type TMissing { get; }

        public MissingDependancyException(Type tCreating, Type tMissing)
        {
            TCreating = tCreating;
            TMissing = tMissing;
        }
    }

}