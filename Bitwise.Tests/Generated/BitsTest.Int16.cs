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
        /// <see cref="Bits.HasAnyFlag(short, short)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagInt16()
        {
            Assert.IsTrue(((short)1).HasAnyFlag(1));
            Assert.IsTrue(((short)3).HasAnyFlag(2));
            Assert.IsFalse(((short)18).HasAnyFlag(9));
            Assert.IsFalse(short.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((short)0).HasAnyFlag(0));
        }

        /// <summary>
        /// <see cref="Bits.HasAllFlags(short, short)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsInt16()
        {
            Assert.IsTrue(((short)1).HasAllFlags(1));
            Assert.IsTrue(((short)3).HasAllFlags(2));
            Assert.IsFalse(((short)18).HasAllFlags(9));
            Assert.IsTrue(short.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((short)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(short, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitInt16()
        {
            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.IsTrue((((short)1) << i).GetBit(i));
                Assert.IsFalse((~(((short)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(short).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(short).GetBit(Bits.SizeOfInt16InBits + 1));
        }

        
    }
}
