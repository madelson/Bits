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
        /// <see cref="Bits.HasAnyFlag(uint, uint)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagUInt32()
        {
            Assert.IsTrue(((uint)1).HasAnyFlag(1));
            Assert.IsTrue(((uint)3).HasAnyFlag(2));
            Assert.IsFalse(((uint)18).HasAnyFlag(9));
            Assert.IsFalse(uint.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((uint)0).HasAnyFlag(0));
        }

        /// <summary>
        /// <see cref="Bits.HasAllFlags(uint, uint)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsUInt32()
        {
            Assert.IsTrue(((uint)1).HasAllFlags(1));
            Assert.IsTrue(((uint)3).HasAllFlags(2));
            Assert.IsFalse(((uint)18).HasAllFlags(9));
            Assert.IsTrue(uint.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((uint)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(uint, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitUInt32()
        {
            for (var i = 0; i < Bits.SizeOfUInt32InBits; ++i)
            {
                Assert.IsTrue((((uint)1) << i).GetBit(i));
                Assert.IsFalse((~(((uint)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(uint).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(uint).GetBit(Bits.SizeOfUInt32InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.SetBit(uint, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitUInt32()
        {
            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);
            var noBitsSet = default(uint);

            for (var i = 0; i < Bits.SizeOfUInt32InBits; ++i)
            {
                Assert.AreEqual((uint)(((uint)1) << i), default(uint).SetBit(i));
                Assert.AreEqual(((uint)25).SetBit(i), ((uint)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
                noBitsSet = noBitsSet.SetBit(i);
            }

            Assert.AreEqual(allBitsSet, noBitsSet);

            Assert.Throws<IndexOutOfRangeException>(() => default(uint).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(uint).SetBit(Bits.SizeOfUInt32InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearBit(uint, int)"/>
        /// </summary>
        [Test]
        public void TestClearBitUInt32()
        {
            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);

            for (var i = 0; i < Bits.SizeOfUInt32InBits; ++i)
            {
                Assert.AreEqual((uint)0, default(uint).ClearBit(i));
                Assert.AreEqual(((uint)25).ClearBit(i), ((uint)25).ClearBit(i).ClearBit(i));
                Assert.AreEqual((uint)~(((uint)1) << i), allBitsSet.ClearBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(uint).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(uint).SetBit(Bits.SizeOfUInt32InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(uint, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitUInt32()
        {
            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);

            for (var i = 0; i < Bits.SizeOfUInt32InBits; ++i)
            {
                Assert.AreEqual((uint)(((uint)1) << i), default(uint).FlipBit(i));
                Assert.AreEqual(((uint)25), ((uint)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((uint)~(((uint)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(uint).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(uint).SetBit(Bits.SizeOfUInt32InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearLeastSignificantBit(uint)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantBitUInt32()
        {
            Assert.AreEqual((uint)0b101000, Bits.ClearLeastSignificantBit((uint)0b101100));
            Assert.AreEqual(default(uint), Bits.ClearLeastSignificantBit(default(uint)));

            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);

            for (var i = 0; i < Bits.SizeOfUInt32InBits; ++i)
            {
                Assert.AreEqual(default(uint), Bits.ClearLeastSignificantBit((uint)((uint)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantBit(allBitsSet);
            }

            Assert.AreEqual(default(uint), allBitsSet);
        }

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantSetBit(uint)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantSetBitUInt32()
        {
            Assert.AreEqual((uint)0b000100, Bits.IsolateLeastSignificantSetBit((uint)0b101100));
            Assert.AreEqual(default(uint), Bits.IsolateLeastSignificantSetBit(default(uint)));

            for (var i = 0; i < Bits.SizeOfUInt32InBits; ++i)
            {
                Assert.AreEqual(default(uint).SetBit(i), Bits.IsolateLeastSignificantSetBit((default(uint).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(uint).SetBit(i - 1), Bits.IsolateLeastSignificantSetBit(default(uint).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantSetBit(uint)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantSetBitUInt32()
        {
            Assert.AreEqual((uint)0b100000, Bits.IsolateMostSignificantSetBit((uint)0b101100));
            Assert.AreEqual(default(uint), Bits.IsolateMostSignificantSetBit(default(uint)));

            for (var i = 0; i < Bits.SizeOfUInt32InBits; ++i)
            {
                Assert.AreEqual(default(uint).SetBit(i), Bits.IsolateMostSignificantSetBit((default(uint).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(uint).SetBit(i), Bits.IsolateMostSignificantSetBit(default(uint).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        
    }
}
