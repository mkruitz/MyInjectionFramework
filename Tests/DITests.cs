using Core;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DITests
    {
        private DIMapper Mapper { get; set; }
        private DIGetter DI { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mapper = new DIMapper();
            DI = new DIGetter(Mapper);
        }

        [Test]
        public void SimpleClass_GetInstance_ReturnsInstance()
        {
            var dummy = DI.Get<SimpleClass>();

            Assert.AreEqual(typeof(SimpleClass), dummy.GetType());
        }

        [Test]
        public void SimpleInterfaceOfClass_GetInstance_Throws()
        {
            var ex = Assert.Throws<MissingMappingException>(() => DI.Get<ISimpleClass>());
            Assert.AreEqual(typeof(ISimpleClass), ex.TMissing);
        }

        [Test]
        public void SimpleInterfaceOfClass_AddedMapping_GetInstance_ReturnsInstance()
        {
            Mapper.Map<ISimpleClass, SimpleClass>();

            var dummy = DI.Get<ISimpleClass>();

            Assert.AreEqual(typeof(SimpleClass), dummy.GetType());
        }

        [Test]
        public void ChildOfSimpleClass_AddedMapping_GetInstance_ChildOfSimpleClass()
        {
            Mapper.Map<SimpleClass, ChildOfSimpleClass>();

            var dummy = DI.Get<SimpleClass>();

            Assert.AreEqual(typeof(ChildOfSimpleClass), dummy.GetType());
        }

        [Test]
        public void SimpleInterfaceOfClass_DoubleMapping_Throws()
        {
            Mapper.Map<ISimpleClass, SimpleClass>();

            var ex = Assert.Throws<DoubleMappingException>(() => Mapper.Map<ISimpleClass, ChildOfSimpleClass>());
            Assert.AreEqual(typeof(ISimpleClass), ex.TRequested);
        }

        [Test]
        public void DependantClass_GetInstance_Throws()
        {
            var ex = Assert.Throws<MissingDependancyException>(() => DI.Get<DependantClass>());
            Assert.AreEqual(typeof(DependantClass), ex.TCreating);
            Assert.AreEqual(typeof(ISimpleClass), ex.TMissing);
        }

        [Test]
        public void DependantClass_AddedMapping_GetInstance_ReturnsInstance()
        {
            Mapper.Map<ISimpleClass, SimpleClass>();

            var dummy =  DI.Get<DependantClass>();

            Assert.AreEqual(typeof(DependantClass), dummy.GetType());
            Assert.AreEqual(0, dummy.GetInjectedValue());
        }
    }
}
