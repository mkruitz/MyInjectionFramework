﻿using Core;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DILifetimeTests
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
        public void SameClass_GetTwice_ReturnsSameInstance()
        {
            var dummy1 = DI.Get<SimpleClass>();
            var dummy2 = DI.Get<SimpleClass>();

            Assert.AreEqual(dummy1, dummy2);
        }
    }
}