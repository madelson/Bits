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
        /// <see cref="Bits.HasAnyFlag(byte, byte)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagByte()
        {
            Assert.IsTrue(((byte)1).HasAnyFlag(1));
            Assert.IsTrue(((byte)3).HasAnyFlag(2));
            Assert.IsFalse(((byte)18).HasAnyFlag(9));
            Assert.IsFalse(byte.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((byte)0).HasAnyFlag(0));
        }

        
    }
}
