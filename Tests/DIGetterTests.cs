using Core;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DIGetterTests
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
        public void GenericClass_GetTwice_ReturnsInstance()
        {
            var dummy = DI.Get<GenericClass<SimpleClass>>();

            Assert.AreEqual(typeof(GenericClass<SimpleClass>), dummy.GetType());
            Assert.AreEqual(typeof(SimpleClass), dummy.GenericObject.GetType());
            Assert.IsNotNull(dummy.GenericObject);
        }
    }
}
