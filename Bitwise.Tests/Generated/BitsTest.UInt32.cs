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

        /// <summary>
        /// <see cref="Bits.HasAllFlags(uint, uint)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsUInt32()
        {
            Assert.IsTrue(((uint)1).HasAllFlags(1));
            Assert.IsTrue(((uint)3).HasAllFlags(2));
            Assert.IsFalse(((uint)18).HasAllFlags(9));
            Assert.IsTrue(uint.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((uint)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(uint, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitUInt32()
        {
            for (var i = 0; i < Bits.SizeOfUInt32InBits; ++i)
            {
                Assert.IsTrue((((uint)1) << i).GetBit(i));
                Assert.IsFalse((~(((uint)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(uint).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(uint).GetBit(Bits.SizeOfUInt32InBits + 1));
        }

        
    }
}
