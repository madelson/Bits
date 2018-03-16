//
// AUTO-GENERATED
//
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Bitwise;

namespace Bitwise.Tests
{
    public partial class BitsTest
    {
        /// <summary>
        /// <see cref="Bits.HasAnyFlag(uint, uint)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagUInt32()
        {
            Assert.IsTrue(((uint)1).HasAnyFlag(1));
            Assert.IsTrue(((uint)3).HasAnyFlag(2));
            Assert.IsFalse(((uint)18).HasAnyFlag(9));
            Assert.IsFalse(uint.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((uint)0).HasAnyFlag(0));
        }

        
    }
}
