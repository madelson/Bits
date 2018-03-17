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
        /// <see cref="Bits.HasAnyFlag(ulong, ulong)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagUInt64()
        {
            Assert.IsTrue(((ulong)1).HasAnyFlag(1));
            Assert.IsTrue(((ulong)3).HasAnyFlag(2));
            Assert.IsFalse(((ulong)18).HasAnyFlag(9));
            Assert.IsFalse(ulong.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((ulong)0).HasAnyFlag(0));
        }

        /// <summary>
        /// <see cref="Bits.HasAllFlags(ulong, ulong)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsUInt64()
        {
            Assert.IsTrue(((ulong)1).HasAllFlags(1));
            Assert.IsTrue(((ulong)3).HasAllFlags(2));
            Assert.IsFalse(((ulong)18).HasAllFlags(9));
            Assert.IsTrue(ulong.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((ulong)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(ulong, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitUInt64()
        {
            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Assert.IsTrue((((ulong)1) << i).GetBit(i));
                Assert.IsFalse((~(((ulong)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(ulong).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(ulong).GetBit(Bits.SizeOfUInt64InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.SetBit(ulong, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitUInt64()
        {
            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            var noBitsSet = default(ulong);

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Assert.AreEqual((ulong)(((ulong)1) << i), default(ulong).SetBit(i));
                Assert.AreEqual(((ulong)25).SetBit(i), ((ulong)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
                noBitsSet = noBitsSet.SetBit(i);
            }

            Assert.AreEqual(allBitsSet, noBitsSet);

            Assert.Throws<IndexOutOfRangeException>(() => default(ulong).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(ulong).SetBit(Bits.SizeOfUInt64InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearBit(ulong, int)"/>
        /// </summary>
        [Test]
        public void TestClearBitUInt64()
        {
            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Assert.AreEqual((ulong)0, default(ulong).ClearBit(i));
                Assert.AreEqual(((ulong)25).ClearBit(i), ((ulong)25).ClearBit(i).ClearBit(i));
                Assert.AreEqual((ulong)~(((ulong)1) << i), allBitsSet.ClearBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(ulong).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(ulong).SetBit(Bits.SizeOfUInt64InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(ulong, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitUInt64()
        {
            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Assert.AreEqual((ulong)(((ulong)1) << i), default(ulong).FlipBit(i));
                Assert.AreEqual(((ulong)25), ((ulong)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((ulong)~(((ulong)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(ulong).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(ulong).SetBit(Bits.SizeOfUInt64InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearLeastSignificantBit(ulong)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantBitUInt64()
        {
            Assert.AreEqual((ulong)0b101000, Bits.ClearLeastSignificantBit((ulong)0b101100));
            Assert.AreEqual(default(ulong), Bits.ClearLeastSignificantBit(default(ulong)));

            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Assert.AreEqual(default(ulong), Bits.ClearLeastSignificantBit((ulong)((ulong)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantBit(allBitsSet);
            }

            Assert.AreEqual(default(ulong), allBitsSet);
        }

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantSetBit(ulong)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantSetBitUInt64()
        {
            Assert.AreEqual((ulong)0b000100, Bits.IsolateLeastSignificantSetBit((ulong)0b101100));
            Assert.AreEqual(default(ulong), Bits.IsolateLeastSignificantSetBit(default(ulong)));

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Assert.AreEqual(default(ulong).SetBit(i), Bits.IsolateLeastSignificantSetBit((default(ulong).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(ulong).SetBit(i - 1), Bits.IsolateLeastSignificantSetBit(default(ulong).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantSetBit(ulong)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantSetBitUInt64()
        {
            Assert.AreEqual((ulong)0b100000, Bits.IsolateMostSignificantSetBit((ulong)0b101100));
            Assert.AreEqual(default(ulong), Bits.IsolateMostSignificantSetBit(default(ulong)));

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Assert.AreEqual(default(ulong).SetBit(i), Bits.IsolateMostSignificantSetBit((default(ulong).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(ulong).SetBit(i), Bits.IsolateMostSignificantSetBit(default(ulong).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.BitCount(ulong)"/>
        /// </summary>
        [Test]
        public void TestBitCountUInt64()
        {
            Assert.AreEqual(0, Bits.BitCount((ulong)0));
            Assert.AreEqual(1, Bits.BitCount((ulong)1));
            Assert.AreEqual(2, Bits.BitCount(default(ulong).SetBit(Bits.SizeOfUInt64InBits / 2).SetBit(Bits.SizeOfUInt64InBits - 1)));

            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            Assert.AreEqual(Bits.SizeOfUInt64InBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfUInt64InBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfUInt64InBits / 2).ClearBit(0)));

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt64();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.TrailingZeroBitCount(ulong)"/>
        /// </summary>
        [Test]
        public void TestTrailingZeroBitCountUInt64()
        {
            Assert.AreEqual(Bits.SizeOfUInt64InBits, Bits.TrailingZeroBitCount((ulong)0));
            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            Assert.AreEqual(0, Bits.TrailingZeroBitCount(allBitsSet));

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(i + 1, Bits.TrailingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt64();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? 8 * sizeof(ulong) : binaryString.Length - binaryString.TrimEnd('0').Length, Bits.TrailingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.ToShortBinaryString(ulong)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringUInt64()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((ulong)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((ulong)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((ulong)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((ulong)0b01010101)));

            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfUInt64InBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(ulong)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringUInt64()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfUInt64InBits), Bits.ToLongBinaryString((ulong)0));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt64InBits - 1) + "1", Bits.ToLongBinaryString((ulong)1));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt64InBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((ulong)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt64InBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((ulong)0b01010101)));

            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfUInt64InBits), Bits.ToLongBinaryString(allBitsSet));
        }

        /// <summary>
        /// Helper buffer for <see cref="GetRandomUInt64"/>
        /// </summary>
        private readonly byte[] _getRandomBufferUInt64 = new byte[sizeof(ulong)];

        /// <summary>
        /// Helper to generate random <see cref="ulong"/> values for fuzz testing
        /// </summary>
        private ulong GetRandomUInt64()
        {
            this._random.Value.NextBytes(this._getRandomBufferUInt64);
            ulong value = 0;
            for (var i = 0; i < sizeof(ulong); ++i)
            {
                value = unchecked((ulong)((ulong)(value << 8) & (ulong)this._getRandomBufferUInt64[i]));
            }
            return value;
        }

        
    }
}
