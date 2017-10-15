using Core;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DITests
    {
        private DIGetter DI;

        [SetUp]
        public void SetUp()
        {
            DI = new DIGetter();
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
            DI.Map<ISimpleClass, SimpleClass>();

            var dummy = DI.Get<ISimpleClass>();

            Assert.AreEqual(typeof(SimpleClass), dummy.GetType());
        }

        [Test]
        public void ChildOfSimpleClass_AddedMapping_GetInstance_ChildOfSimpleClass()
        {
            DI.Map<SimpleClass, ChildOfSimpleClass>();

            var dummy = DI.Get<SimpleClass>();

            Assert.AreEqual(typeof(ChildOfSimpleClass), dummy.GetType());
        }

        [Test]
        public void SimpleInterfaceOfClass_DoubleMapping_Throws()
        {
            DI.Map<ISimpleClass, SimpleClass>();

            var ex = Assert.Throws<DoubleMappingException>(() => DI.Map<ISimpleClass, ChildOfSimpleClass>());
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
            DI.Map<ISimpleClass, SimpleClass>();

            var dummy =  DI.Get<DependantClass>();

            Assert.AreEqual(typeof(DependantClass), dummy.GetType());
            Assert.AreEqual(0, dummy.GetInjectedValue());
        }
    }
}
