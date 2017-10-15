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
        public ISimpleClass Dependancy { get; }

        public DependantClass(ISimpleClass dependancy)
        {
            Dependancy = dependancy;
        }

        public int GetInjectedValue()
        {
            return Dependancy.Value;
        }
    }
}
