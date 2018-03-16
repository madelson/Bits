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
        /// <see cref="Bits.ClearAllButLeastSignificantBit(int)"/>
        /// </summary>
        [Test]
        public void TestClearAllButLeastSignificantBitInt32()
        {
            Assert.AreEqual((int)0b000100, Bits.ClearAllButLeastSignificantBit((int)0b101100));
            Assert.AreEqual(default(int), Bits.ClearAllButLeastSignificantBit(default(int)));

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.AreEqual(default(int).SetBit(i), Bits.ClearAllButLeastSignificantBit((default(int).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(int).SetBit(i - 1), Bits.ClearAllButLeastSignificantBit(default(int).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.ClearAllButMostSignificantBit(int)"/>
        /// </summary>
        [Test]
        public void TestClearAllButMostSignificantBitInt32()
        {
            Assert.AreEqual((int)0b100000, Bits.ClearAllButMostSignificantBit((int)0b101100));
            Assert.AreEqual(default(int), Bits.ClearAllButMostSignificantBit(default(int)));

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.AreEqual(default(int).SetBit(i), Bits.ClearAllButMostSignificantBit((default(int).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(int).SetBit(i), Bits.ClearAllButMostSignificantBit(default(int).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        
    }
}
