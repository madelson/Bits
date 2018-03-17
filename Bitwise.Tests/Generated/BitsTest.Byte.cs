//
// AUTO-GENERATED
//
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Bitwise;
using System.Linq;

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
        /// <see cref="Bits.IsolateLeastSignificantSetBit(byte)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantSetBitByte()
        {
            Assert.AreEqual((byte)0b000100, Bits.IsolateLeastSignificantSetBit((byte)0b101100));
            Assert.AreEqual(default(byte), Bits.IsolateLeastSignificantSetBit(default(byte)));

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual(default(byte).SetBit(i), Bits.IsolateLeastSignificantSetBit((default(byte).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(byte).SetBit(i - 1), Bits.IsolateLeastSignificantSetBit(default(byte).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantSetBit(byte)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantSetBitByte()
        {
            Assert.AreEqual((byte)0b100000, Bits.IsolateMostSignificantSetBit((byte)0b101100));
            Assert.AreEqual(default(byte), Bits.IsolateMostSignificantSetBit(default(byte)));

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual(default(byte).SetBit(i), Bits.IsolateMostSignificantSetBit((default(byte).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(byte).SetBit(i), Bits.IsolateMostSignificantSetBit(default(byte).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.BitCount(byte)"/>
        /// </summary>
        [Test]
        public void TestBitCountByte()
        {
            Assert.AreEqual(0, Bits.BitCount((byte)0));
            Assert.AreEqual(1, Bits.BitCount((byte)1));
            Assert.AreEqual(2, Bits.BitCount(default(byte).SetBit(Bits.SizeOfByteInBits / 2).SetBit(Bits.SizeOfByteInBits - 1)));

            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Assert.AreEqual(Bits.SizeOfByteInBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfByteInBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfByteInBits / 2).ClearBit(0)));

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomByte();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.TrailingZeroBitCount(byte)"/>
        /// </summary>
        [Test]
        public void TestTrailingZeroBitCountByte()
        {
            Assert.AreEqual(Bits.SizeOfByteInBits, Bits.TrailingZeroBitCount((byte)0));
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Assert.AreEqual(0, Bits.TrailingZeroBitCount(allBitsSet));

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(i + 1, Bits.TrailingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomByte();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfByteInBits : binaryString.Length - binaryString.TrimEnd('0').Length, Bits.TrailingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.LeadingZeroBitCount(byte)"/>
        /// </summary>
        [Test]
        public void TestLeadingZeroBitCountByte()
        {
            Assert.AreEqual(Bits.SizeOfByteInBits, Bits.LeadingZeroBitCount((byte)0));
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Assert.AreEqual(0, Bits.LeadingZeroBitCount(allBitsSet));

            for (var i = Bits.SizeOfByteInBits - 1; i >= 0; --i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(Bits.SizeOfByteInBits - i, Bits.LeadingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomByte();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfByteInBits : Bits.SizeOfByteInBits - binaryString.Length, Bits.LeadingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.ToShortBinaryString(byte)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringByte()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((byte)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((byte)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((byte)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((byte)0b01010101)));

            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfByteInBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(byte)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringByte()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfByteInBits), Bits.ToLongBinaryString((byte)0));
            Assert.AreEqual(new string('0', Bits.SizeOfByteInBits - 1) + "1", Bits.ToLongBinaryString((byte)1));
            Assert.AreEqual(new string('0', Bits.SizeOfByteInBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((byte)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfByteInBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((byte)0b01010101)));

            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfByteInBits), Bits.ToLongBinaryString(allBitsSet));
        }

        /// <summary>
        /// Helper buffer for <see cref="GetRandomByte"/>
        /// </summary>
        private readonly byte[] _getRandomBufferByte = new byte[sizeof(byte)];

        /// <summary>
        /// Helper to generate random <see cref="byte"/> values for fuzz testing
        /// </summary>
        private byte GetRandomByte()
        {
            this._random.Value.NextBytes(this._getRandomBufferByte);
            byte value = 0;
            for (var i = 0; i < sizeof(byte); ++i)
            {
                value = unchecked((byte)((byte)(value << 8) & (byte)this._getRandomBufferByte[i]));
            }
            return value;
        }

        
    }
}
