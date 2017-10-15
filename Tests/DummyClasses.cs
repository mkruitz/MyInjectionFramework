using System.Runtime.InteropServices.WindowsRuntime;

namespace Tests
{
    public interface ISimpleClass
    {
        int Value { get; }
    }

    public class SimpleClass : ISimpleClass
    {
        public int Value{ get; set; }
    }

    public class ChildOfSimpleClass : SimpleClass { }

    public class DependantClass
    {
        private readonly ISimpleClass _dependancy;

        public DependantClass(ISimpleClass dependancy)
        {
            _dependancy = dependancy;
        }

        public int GetInjectedValue()
        {
            return _dependancy.Value;
        }
    }
}
