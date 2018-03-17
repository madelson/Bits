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
            var noBitsSet = default(ushort);

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual((ushort)(((ushort)1) << i), default(ushort).SetBit(i));
                Assert.AreEqual(((ushort)25).SetBit(i), ((ushort)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
                noBitsSet = noBitsSet.SetBit(i);
            }

            Assert.AreEqual(allBitsSet, noBitsSet);

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

        /// <summary>
        /// <see cref="Bits.ClearLeastSignificantBit(ushort)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantBitUInt16()
        {
            Assert.AreEqual((ushort)0b101000, Bits.ClearLeastSignificantBit((ushort)0b101100));
            Assert.AreEqual(default(ushort), Bits.ClearLeastSignificantBit(default(ushort)));

            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual(default(ushort), Bits.ClearLeastSignificantBit((ushort)((ushort)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantBit(allBitsSet);
            }

            Assert.AreEqual(default(ushort), allBitsSet);
        }

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantSetBit(ushort)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantSetBitUInt16()
        {
            Assert.AreEqual((ushort)0b000100, Bits.IsolateLeastSignificantSetBit((ushort)0b101100));
            Assert.AreEqual(default(ushort), Bits.IsolateLeastSignificantSetBit(default(ushort)));

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual(default(ushort).SetBit(i), Bits.IsolateLeastSignificantSetBit((default(ushort).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(ushort).SetBit(i - 1), Bits.IsolateLeastSignificantSetBit(default(ushort).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantSetBit(ushort)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantSetBitUInt16()
        {
            Assert.AreEqual((ushort)0b100000, Bits.IsolateMostSignificantSetBit((ushort)0b101100));
            Assert.AreEqual(default(ushort), Bits.IsolateMostSignificantSetBit(default(ushort)));

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual(default(ushort).SetBit(i), Bits.IsolateMostSignificantSetBit((default(ushort).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(ushort).SetBit(i), Bits.IsolateMostSignificantSetBit(default(ushort).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.BitCount(ushort)"/>
        /// </summary>
        [Test]
        public void TestBitCountUInt16()
        {
            Assert.AreEqual(0, Bits.BitCount((ushort)0));
            Assert.AreEqual(1, Bits.BitCount((ushort)1));
            Assert.AreEqual(2, Bits.BitCount(default(ushort).SetBit(Bits.SizeOfUInt16InBits / 2).SetBit(Bits.SizeOfUInt16InBits - 1)));

            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Assert.AreEqual(Bits.SizeOfUInt16InBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfUInt16InBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfUInt16InBits / 2).ClearBit(0)));

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt16();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.TrailingZeroBitCount(ushort)"/>
        /// </summary>
        [Test]
        public void TestTrailingZeroBitCountUInt16()
        {
            Assert.AreEqual(Bits.SizeOfUInt16InBits, Bits.TrailingZeroBitCount((ushort)0));
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Assert.AreEqual(0, Bits.TrailingZeroBitCount(allBitsSet));

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(i + 1, Bits.TrailingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt16();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? 8 * sizeof(ushort) : binaryString.Length - binaryString.TrimEnd('0').Length, Bits.TrailingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.ToShortBinaryString(ushort)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringUInt16()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((ushort)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((ushort)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((ushort)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((ushort)0b01010101)));

            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfUInt16InBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(ushort)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringUInt16()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfUInt16InBits), Bits.ToLongBinaryString((ushort)0));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt16InBits - 1) + "1", Bits.ToLongBinaryString((ushort)1));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt16InBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((ushort)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt16InBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((ushort)0b01010101)));

            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfUInt16InBits), Bits.ToLongBinaryString(allBitsSet));
        }

        /// <summary>
        /// Helper buffer for <see cref="GetRandomUInt16"/>
        /// </summary>
        private readonly byte[] _getRandomBufferUInt16 = new byte[sizeof(ushort)];

        /// <summary>
        /// Helper to generate random <see cref="ushort"/> values for fuzz testing
        /// </summary>
        private ushort GetRandomUInt16()
        {
            this._random.Value.NextBytes(this._getRandomBufferUInt16);
            ushort value = 0;
            for (var i = 0; i < sizeof(ushort); ++i)
            {
                value = unchecked((ushort)((ushort)(value << 8) & (ushort)this._getRandomBufferUInt16[i]));
            }
            return value;
        }

        
    }
}
