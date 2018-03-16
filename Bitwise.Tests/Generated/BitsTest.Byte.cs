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
            var noBitsSet = default(byte);

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual((byte)(((byte)1) << i), default(byte).SetBit(i));
                Assert.AreEqual(((byte)25).SetBit(i), ((byte)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
                noBitsSet = noBitsSet.SetBit(i);
            }

            Assert.AreEqual(allBitsSet, noBitsSet);

            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(Bits.SizeOfByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearBit(byte, int)"/>
        /// </summary>
        [Test]
        public void TestClearBitByte()
        {
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual((byte)0, default(byte).ClearBit(i));
                Assert.AreEqual(((byte)25).ClearBit(i), ((byte)25).ClearBit(i).ClearBit(i));
                Assert.AreEqual((byte)~(((byte)1) << i), allBitsSet.ClearBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(Bits.SizeOfByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(byte, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitByte()
        {
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual((byte)(((byte)1) << i), default(byte).FlipBit(i));
                Assert.AreEqual(((byte)25), ((byte)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((byte)~(((byte)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(Bits.SizeOfByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearLeastSignificantBit(byte)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantBitByte()
        {
            Assert.AreEqual((byte)0b101000, Bits.ClearLeastSignificantBit((byte)0b101100));
            Assert.AreEqual(default(byte), Bits.ClearLeastSignificantBit(default(byte)));

            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual(default(byte), Bits.ClearLeastSignificantBit((byte)((byte)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantBit(allBitsSet);
            }

            Assert.AreEqual(default(byte), allBitsSet);
        }

        /// <summary>
        /// <see cref="Bits.ClearAllButLeastSignificantBit(byte)"/>
        /// </summary>
        [Test]
        public void TestClearAllButLeastSignificantBitByte()
        {
            Assert.AreEqual((byte)0b000100, Bits.ClearAllButLeastSignificantBit((byte)0b101100));
            Assert.AreEqual(default(byte), Bits.ClearAllButLeastSignificantBit(default(byte)));

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual(default(byte).SetBit(i), Bits.ClearAllButLeastSignificantBit((default(byte).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(byte).SetBit(i - 1), Bits.ClearAllButLeastSignificantBit(default(byte).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.ClearAllButMostSignificantBit(byte)"/>
        /// </summary>
        [Test]
        public void TestClearAllButMostSignificantBitByte()
        {
            Assert.AreEqual((byte)0b100000, Bits.ClearAllButMostSignificantBit((byte)0b101100));
            Assert.AreEqual(default(byte), Bits.ClearAllButMostSignificantBit(default(byte)));

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual(default(byte).SetBit(i), Bits.ClearAllButMostSignificantBit((default(byte).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(byte).SetBit(i), Bits.ClearAllButMostSignificantBit(default(byte).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        
    }
}
