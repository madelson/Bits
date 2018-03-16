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

        /// <summary>
        /// <see cref="Bits.HasAllFlags(byte, byte)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsByte()
        {
            Assert.IsTrue(((byte)1).HasAllFlags(1));
            Assert.IsTrue(((byte)3).HasAllFlags(2));
            Assert.IsFalse(((byte)18).HasAllFlags(9));
            Assert.IsTrue(byte.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((byte)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(byte, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitByte()
        {
            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.IsTrue((((byte)1) << i).GetBit(i));
                Assert.IsFalse((~(((byte)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(byte).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(byte).GetBit(Bits.SizeOfByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.SetBit(byte, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitByte()
        {
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual((byte)(((byte)1) << i), default(byte).SetBit(i));
                Assert.AreEqual(((byte)25).SetBit(i), ((byte)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(Bits.SizeOfByteInBits + 1));
        }

        
    }
}
