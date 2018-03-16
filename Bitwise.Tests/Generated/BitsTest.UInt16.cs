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
        /// <see cref="Bits.HasAnyFlag(ushort, ushort)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagUInt16()
        {
            Assert.IsTrue(((ushort)1).HasAnyFlag(1));
            Assert.IsTrue(((ushort)3).HasAnyFlag(2));
            Assert.IsFalse(((ushort)18).HasAnyFlag(9));
            Assert.IsFalse(ushort.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((ushort)0).HasAnyFlag(0));
        }

        /// <summary>
        /// <see cref="Bits.HasAllFlags(ushort, ushort)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsUInt16()
        {
            Assert.IsTrue(((ushort)1).HasAllFlags(1));
            Assert.IsTrue(((ushort)3).HasAllFlags(2));
            Assert.IsFalse(((ushort)18).HasAllFlags(9));
            Assert.IsTrue(ushort.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((ushort)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(ushort, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitUInt16()
        {
            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.IsTrue((((ushort)1) << i).GetBit(i));
                Assert.IsFalse((~(((ushort)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).GetBit(Bits.SizeOfUInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.SetBit(ushort, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitUInt16()
        {
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual((ushort)(((ushort)1) << i), default(ushort).SetBit(i));
                Assert.AreEqual(((ushort)25).SetBit(i), ((ushort)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).SetBit(Bits.SizeOfUInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearBit(ushort, int)"/>
        /// </summary>
        [Test]
        public void TestClearBitUInt16()
        {
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual((ushort)0, default(ushort).ClearBit(i));
                Assert.AreEqual(((ushort)25).ClearBit(i), ((ushort)25).ClearBit(i).ClearBit(i));
                Assert.AreEqual((ushort)~(((ushort)1) << i), allBitsSet.ClearBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).SetBit(Bits.SizeOfUInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(ushort, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitUInt16()
        {
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual((ushort)(((ushort)1) << i), default(ushort).FlipBit(i));
                Assert.AreEqual(((ushort)25), ((ushort)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((ushort)~(((ushort)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).SetBit(Bits.SizeOfUInt16InBits + 1));
        }

        
    }
}
