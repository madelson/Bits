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
        /// <see cref="Bits.HasAnyFlag(sbyte, sbyte)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagSByte()
        {
            Assert.IsTrue(((sbyte)1).HasAnyFlag(1));
            Assert.IsTrue(((sbyte)3).HasAnyFlag(2));
            Assert.IsFalse(((sbyte)18).HasAnyFlag(9));
            Assert.IsFalse(sbyte.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((sbyte)0).HasAnyFlag(0));
        }

        /// <summary>
        /// <see cref="Bits.HasAllFlags(sbyte, sbyte)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsSByte()
        {
            Assert.IsTrue(((sbyte)1).HasAllFlags(1));
            Assert.IsTrue(((sbyte)3).HasAllFlags(2));
            Assert.IsFalse(((sbyte)18).HasAllFlags(9));
            Assert.IsTrue(sbyte.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((sbyte)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(sbyte, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitSByte()
        {
            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.IsTrue((((sbyte)1) << i).GetBit(i));
                Assert.IsFalse((~(((sbyte)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).GetBit(Bits.SizeOfSByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.SetBit(sbyte, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitSByte()
        {
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual((sbyte)(((sbyte)1) << i), default(sbyte).SetBit(i));
                Assert.AreEqual(((sbyte)25).SetBit(i), ((sbyte)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).SetBit(Bits.SizeOfSByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearBit(sbyte, int)"/>
        /// </summary>
        [Test]
        public void TestClearBitSByte()
        {
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual((sbyte)0, default(sbyte).ClearBit(i));
                Assert.AreEqual(((sbyte)25).ClearBit(i), ((sbyte)25).ClearBit(i).ClearBit(i));
                Assert.AreEqual((sbyte)~(((sbyte)1) << i), allBitsSet.ClearBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).SetBit(Bits.SizeOfSByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(sbyte, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitSByte()
        {
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual((sbyte)(((sbyte)1) << i), default(sbyte).FlipBit(i));
                Assert.AreEqual(((sbyte)25), ((sbyte)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((sbyte)~(((sbyte)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).SetBit(Bits.SizeOfSByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearLeastSignificantBit(sbyte)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantBitSByte()
        {
            Assert.AreEqual((sbyte)0b101000, Bits.ClearLeastSignificantBit((sbyte)0b101100));
            Assert.AreEqual(default(sbyte), Bits.ClearLeastSignificantBit(default(sbyte)));

            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual(default(sbyte), Bits.ClearLeastSignificantBit((sbyte)((sbyte)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantBit(allBitsSet);
            }

            Assert.AreEqual(default(sbyte), allBitsSet);
        }

        
    }
}
