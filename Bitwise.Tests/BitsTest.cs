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
        /// <see cref="Bits.HasAnyFlag(long, long)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagInt64()
        {
            Assert.IsTrue(((long)1).HasAnyFlag(1));
            Assert.IsTrue(((long)3).HasAnyFlag(2));
            Assert.IsFalse(((long)18).HasAnyFlag(9));
            Assert.IsFalse(long.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((long)0).HasAnyFlag(0));
        }

        /// <summary>
        /// <see cref="Bits.HasAllFlags(long, long)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsInt64()
        {
            Assert.IsTrue(((long)1).HasAllFlags(1));
            Assert.IsTrue(((long)3).HasAllFlags(2));
            Assert.IsFalse(((long)18).HasAllFlags(9));
            Assert.IsTrue(long.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((long)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(long, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitInt64()
        {
            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Assert.IsTrue((((long)1) << i).GetBit(i));
                Assert.IsFalse((~(((long)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(long).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(long).GetBit(Bits.SizeOfInt64InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.SetBit(long, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitInt64()
        {
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);

            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Assert.AreEqual((long)(((long)1) << i), default(long).SetBit(i));
                Assert.AreEqual(((long)25).SetBit(i), ((long)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(long).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(long).SetBit(Bits.SizeOfInt64InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(long, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitInt64()
        {
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);

            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Assert.AreEqual((long)(((long)1) << i), default(long).FlipBit(i));
                Assert.AreEqual(((long)25), ((long)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((long)~(((long)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(long).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(long).SetBit(Bits.SizeOfInt64InBits + 1));
        }

        // END MEMBERS
    }
}
