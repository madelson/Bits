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
            var noBitsSet = default(sbyte);

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual((sbyte)(((sbyte)1) << i), default(sbyte).SetBit(i));
                Assert.AreEqual(((sbyte)25).SetBit(i), ((sbyte)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
                noBitsSet = noBitsSet.SetBit(i);
            }

            Assert.AreEqual(allBitsSet, noBitsSet);

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

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantSetBit(sbyte)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantSetBitSByte()
        {
            Assert.AreEqual((sbyte)0b000100, Bits.IsolateLeastSignificantSetBit((sbyte)0b101100));
            Assert.AreEqual(default(sbyte), Bits.IsolateLeastSignificantSetBit(default(sbyte)));

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual(default(sbyte).SetBit(i), Bits.IsolateLeastSignificantSetBit((default(sbyte).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(sbyte).SetBit(i - 1), Bits.IsolateLeastSignificantSetBit(default(sbyte).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantSetBit(sbyte)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantSetBitSByte()
        {
            Assert.AreEqual((sbyte)0b100000, Bits.IsolateMostSignificantSetBit((sbyte)0b101100));
            Assert.AreEqual(default(sbyte), Bits.IsolateMostSignificantSetBit(default(sbyte)));

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual(default(sbyte).SetBit(i), Bits.IsolateMostSignificantSetBit((default(sbyte).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(sbyte).SetBit(i), Bits.IsolateMostSignificantSetBit(default(sbyte).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.BitCount(sbyte)"/>
        /// </summary>
        [Test]
        public void TestBitCountSByte()
        {
            Assert.AreEqual(0, Bits.BitCount((sbyte)0));
            Assert.AreEqual(1, Bits.BitCount((sbyte)1));
            Assert.AreEqual(2, Bits.BitCount(default(sbyte).SetBit(Bits.SizeOfSByteInBits / 2).SetBit(Bits.SizeOfSByteInBits - 1)));

            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Assert.AreEqual(Bits.SizeOfSByteInBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfSByteInBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfSByteInBits / 2).ClearBit(0)));

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomSByte();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.TrailingZeroBitCount(sbyte)"/>
        /// </summary>
        [Test]
        public void TestTrailingZeroBitCountSByte()
        {
            Assert.AreEqual(Bits.SizeOfSByteInBits, Bits.TrailingZeroBitCount((sbyte)0));
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Assert.AreEqual(0, Bits.TrailingZeroBitCount(allBitsSet));

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(i + 1, Bits.TrailingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomSByte();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? 8 * sizeof(sbyte) : binaryString.Length - binaryString.TrimEnd('0').Length, Bits.TrailingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.ToShortBinaryString(sbyte)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringSByte()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((sbyte)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((sbyte)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((sbyte)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((sbyte)0b01010101)));

            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfSByteInBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(sbyte)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringSByte()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfSByteInBits), Bits.ToLongBinaryString((sbyte)0));
            Assert.AreEqual(new string('0', Bits.SizeOfSByteInBits - 1) + "1", Bits.ToLongBinaryString((sbyte)1));
            Assert.AreEqual(new string('0', Bits.SizeOfSByteInBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((sbyte)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfSByteInBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((sbyte)0b01010101)));

            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfSByteInBits), Bits.ToLongBinaryString(allBitsSet));
        }

        /// <summary>
        /// Helper buffer for <see cref="GetRandomSByte"/>
        /// </summary>
        private readonly byte[] _getRandomBufferSByte = new byte[sizeof(sbyte)];

        /// <summary>
        /// Helper to generate random <see cref="sbyte"/> values for fuzz testing
        /// </summary>
        private sbyte GetRandomSByte()
        {
            this._random.Value.NextBytes(this._getRandomBufferSByte);
            sbyte value = 0;
            for (var i = 0; i < sizeof(sbyte); ++i)
            {
                value = unchecked((sbyte)((sbyte)(value << 8) & (sbyte)this._getRandomBufferSByte[i]));
            }
            return value;
        }

        
    }
}
