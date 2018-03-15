using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Bitwise;

namespace Bitwise.Tests
{
    [TestFixture]
    public class BitsTest
    {
        [Test]
        public void TestHasAnyFlagInt64()
        {
            Assert.AreEqual(true, ((long)1).HasAnyFlag(1));
        }
    }
}
