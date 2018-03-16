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
        /// <see cref="Bits.HasAnyFlag(int, int)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagInt32()
        {
            Assert.IsTrue(((int)1).HasAnyFlag(1));
            Assert.IsTrue(((int)3).HasAnyFlag(2));
            Assert.IsFalse(((int)18).HasAnyFlag(9));
            Assert.IsFalse(int.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((int)0).HasAnyFlag(0));
        }

        /// <summary>
        /// <see cref="Bits.HasAllFlags(int, int)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsInt32()
        {
            Assert.IsTrue(((int)1).HasAllFlags(1));
            Assert.IsTrue(((int)3).HasAllFlags(2));
            Assert.IsFalse(((int)18).HasAllFlags(9));
            Assert.IsTrue(int.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((int)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(int, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitInt32()
        {
            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.IsTrue((((int)1) << i).GetBit(i));
                Assert.IsFalse((~(((int)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(int).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(int).GetBit(Bits.SizeOfInt32InBits + 1));
        }

        
    }
}
