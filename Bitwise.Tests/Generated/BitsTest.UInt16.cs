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

        
    }
}
