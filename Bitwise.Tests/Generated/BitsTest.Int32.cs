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
        /// <see cref="Bits.HasAnyFlag(int, int)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagInt32()
        {
            Assert.IsTrue(((int)1).HasAnyFlag(1));
            Assert.IsTrue(((int)3).HasAnyFlag(2));
            Assert.IsFalse(((int)18).HasAnyFlag(9));
            Assert.IsFalse(int.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((int)0).HasAnyFlag(0));
        }

        /// <summary>
        /// <see cref="Bits.HasAllFlags(int, int)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsInt32()
        {
            Assert.IsTrue(((int)1).HasAllFlags(1));
            Assert.IsTrue(((int)3).HasAllFlags(2));
            Assert.IsFalse(((int)18).HasAllFlags(9));
            Assert.IsTrue(int.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((int)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(int, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitInt32()
        {
            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.IsTrue((((int)1) << i).GetBit(i));
                Assert.IsFalse((~(((int)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(int).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(int).GetBit(Bits.SizeOfInt32InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.SetBit(int, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitInt32()
        {
            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);
            var noBitsSet = default(int);

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.AreEqual((int)(((int)1) << i), default(int).SetBit(i));
                Assert.AreEqual(((int)25).SetBit(i), ((int)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
                noBitsSet = noBitsSet.SetBit(i);
            }

            Assert.AreEqual(allBitsSet, noBitsSet);

            Assert.Throws<IndexOutOfRangeException>(() => default(int).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(int).SetBit(Bits.SizeOfInt32InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearBit(int, int)"/>
        /// </summary>
        [Test]
        public void TestClearBitInt32()
        {
            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.AreEqual((int)0, default(int).ClearBit(i));
                Assert.AreEqual(((int)25).ClearBit(i), ((int)25).ClearBit(i).ClearBit(i));
                Assert.AreEqual((int)~(((int)1) << i), allBitsSet.ClearBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(int).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(int).SetBit(Bits.SizeOfInt32InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(int, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitInt32()
        {
            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.AreEqual((int)(((int)1) << i), default(int).FlipBit(i));
                Assert.AreEqual(((int)25), ((int)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((int)~(((int)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(int).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(int).SetBit(Bits.SizeOfInt32InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearLeastSignificantBit(int)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantBitInt32()
        {
            Assert.AreEqual((int)0b101000, Bits.ClearLeastSignificantBit((int)0b101100));
            Assert.AreEqual(default(int), Bits.ClearLeastSignificantBit(default(int)));

            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.AreEqual(default(int), Bits.ClearLeastSignificantBit((int)((int)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantBit(allBitsSet);
            }

            Assert.AreEqual(default(int), allBitsSet);
        }

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantSetBit(int)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantSetBitInt32()
        {
            Assert.AreEqual((int)0b000100, Bits.IsolateLeastSignificantSetBit((int)0b101100));
            Assert.AreEqual(default(int), Bits.IsolateLeastSignificantSetBit(default(int)));

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.AreEqual(default(int).SetBit(i), Bits.IsolateLeastSignificantSetBit((default(int).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(int).SetBit(i - 1), Bits.IsolateLeastSignificantSetBit(default(int).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantSetBit(int)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantSetBitInt32()
        {
            Assert.AreEqual((int)0b100000, Bits.IsolateMostSignificantSetBit((int)0b101100));
            Assert.AreEqual(default(int), Bits.IsolateMostSignificantSetBit(default(int)));

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.AreEqual(default(int).SetBit(i), Bits.IsolateMostSignificantSetBit((default(int).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(int).SetBit(i), Bits.IsolateMostSignificantSetBit(default(int).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.ToShortBinaryString(int)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringInt32()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((int)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((int)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((int)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((int)0b01010101)));

            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfInt32InBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(int)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringInt32()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfInt32InBits), Bits.ToLongBinaryString((int)0));
            Assert.AreEqual(new string('0', Bits.SizeOfInt32InBits - 1) + "1", Bits.ToLongBinaryString((int)1));
            Assert.AreEqual(new string('0', Bits.SizeOfInt32InBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((int)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfInt32InBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((int)0b01010101)));

            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfInt32InBits), Bits.ToLongBinaryString(allBitsSet));
        }

        
    }
}
