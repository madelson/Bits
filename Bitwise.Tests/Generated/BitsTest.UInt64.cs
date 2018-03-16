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
        /// <see cref="Bits.ClearAllButLeastSignificantBit(ulong)"/>
        /// </summary>
        [Test]
        public void TestClearAllButLeastSignificantBitUInt64()
        {
            Assert.AreEqual((ulong)0b000100, Bits.ClearAllButLeastSignificantBit((ulong)0b101100));
            Assert.AreEqual(default(ulong), Bits.ClearAllButLeastSignificantBit(default(ulong)));

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Assert.AreEqual(default(ulong).SetBit(i), Bits.ClearAllButLeastSignificantBit((default(ulong).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(ulong).SetBit(i - 1), Bits.ClearAllButLeastSignificantBit(default(ulong).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.ClearAllButMostSignificantBit(ulong)"/>
        /// </summary>
        [Test]
        public void TestClearAllButMostSignificantBitUInt64()
        {
            Assert.AreEqual((ulong)0b100000, Bits.ClearAllButMostSignificantBit((ulong)0b101100));
            Assert.AreEqual(default(ulong), Bits.ClearAllButMostSignificantBit(default(ulong)));

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Assert.AreEqual(default(ulong).SetBit(i), Bits.ClearAllButMostSignificantBit((default(ulong).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(ulong).SetBit(i), Bits.ClearAllButMostSignificantBit(default(ulong).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        
    }
}
