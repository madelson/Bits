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

        
    }
}
