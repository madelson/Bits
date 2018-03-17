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

        /// <summary>
        /// <see cref="Bits.SetBit(short, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitInt16()
        {
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            var noBitsSet = default(short);

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual((short)(((short)1) << i), default(short).SetBit(i));
                Assert.AreEqual(((short)25).SetBit(i), ((short)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
                noBitsSet = noBitsSet.SetBit(i);
            }

            Assert.AreEqual(allBitsSet, noBitsSet);

            Assert.Throws<IndexOutOfRangeException>(() => default(short).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(short).SetBit(Bits.SizeOfInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearBit(short, int)"/>
        /// </summary>
        [Test]
        public void TestClearBitInt16()
        {
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual((short)0, default(short).ClearBit(i));
                Assert.AreEqual(((short)25).ClearBit(i), ((short)25).ClearBit(i).ClearBit(i));
                Assert.AreEqual((short)~(((short)1) << i), allBitsSet.ClearBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(short).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(short).SetBit(Bits.SizeOfInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(short, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitInt16()
        {
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual((short)(((short)1) << i), default(short).FlipBit(i));
                Assert.AreEqual(((short)25), ((short)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((short)~(((short)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(short).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(short).SetBit(Bits.SizeOfInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearLeastSignificantBit(short)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantBitInt16()
        {
            Assert.AreEqual((short)0b101000, Bits.ClearLeastSignificantBit((short)0b101100));
            Assert.AreEqual(default(short), Bits.ClearLeastSignificantBit(default(short)));

            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual(default(short), Bits.ClearLeastSignificantBit((short)((short)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantBit(allBitsSet);
            }

            Assert.AreEqual(default(short), allBitsSet);
        }

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantSetBit(short)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantSetBitInt16()
        {
            Assert.AreEqual((short)0b000100, Bits.IsolateLeastSignificantSetBit((short)0b101100));
            Assert.AreEqual(default(short), Bits.IsolateLeastSignificantSetBit(default(short)));

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual(default(short).SetBit(i), Bits.IsolateLeastSignificantSetBit((default(short).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(short).SetBit(i - 1), Bits.IsolateLeastSignificantSetBit(default(short).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantSetBit(short)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantSetBitInt16()
        {
            Assert.AreEqual((short)0b100000, Bits.IsolateMostSignificantSetBit((short)0b101100));
            Assert.AreEqual(default(short), Bits.IsolateMostSignificantSetBit(default(short)));

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual(default(short).SetBit(i), Bits.IsolateMostSignificantSetBit((default(short).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(short).SetBit(i), Bits.IsolateMostSignificantSetBit(default(short).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.BitCount(short)"/>
        /// </summary>
        [Test]
        public void TestBitCountInt16()
        {
            Assert.AreEqual(0, Bits.BitCount((short)0));
            Assert.AreEqual(1, Bits.BitCount((short)1));
            Assert.AreEqual(2, Bits.BitCount(default(short).SetBit(Bits.SizeOfInt16InBits / 2).SetBit(Bits.SizeOfInt16InBits - 1)));

            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            Assert.AreEqual(Bits.SizeOfInt16InBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfInt16InBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfInt16InBits / 2).ClearBit(0)));

            // fuzz testing
            var random = new Random(12345);
            var buffer = new byte[sizeof(short)];
            short GetRandom()
            {
                random.NextBytes(buffer);
                short value = 0;
                for (var i = 0; i < buffer.Length; ++i)
                {
                    value = unchecked((short)((short)(value << 8) & (short)buffer[i]));
                }
                return value;
            }

            for (var i = 0; i < 2000; ++i)
            {
                var randomValue = GetRandom();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.ToShortBinaryString(short)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringInt16()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((short)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((short)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((short)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((short)0b01010101)));

            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfInt16InBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(short)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringInt16()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfInt16InBits), Bits.ToLongBinaryString((short)0));
            Assert.AreEqual(new string('0', Bits.SizeOfInt16InBits - 1) + "1", Bits.ToLongBinaryString((short)1));
            Assert.AreEqual(new string('0', Bits.SizeOfInt16InBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((short)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfInt16InBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((short)0b01010101)));

            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfInt16InBits), Bits.ToLongBinaryString(allBitsSet));
        }

        
    }
}
