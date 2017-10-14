namespace Tests
{
    public interface ISimpleClass { }

    public class SimpleClass : ISimpleClass { }
    public class ChildOfSimpleClass : SimpleClass { }

    public class DependantClass
    {
        public DependantClass(ISimpleClass dependancy) { }
    }
}
