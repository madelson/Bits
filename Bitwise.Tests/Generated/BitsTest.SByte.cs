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

        
    }
}
