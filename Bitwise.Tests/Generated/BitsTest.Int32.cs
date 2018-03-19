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
        /// <see cref="Bits.ShiftLeft(int, int)"/>
        /// </summary>
        [Test]
        public void TestShiftLeftInt32() => this.TestShiftInt32Helper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.ShiftRight(int, int)"/>
        /// </summary>
        [Test]
        public void TestShiftRightInt32() => this.TestShiftInt32Helper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestShiftLeftInt32"/> and <see cref="TestShiftRightInt32"/>
        /// </summary>
        private void TestShiftInt32Helper(bool isLeft)
        {
            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfInt32InBits; i < Bits.SizeOfInt32InBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt32();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(int value, int positions)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = ShiftString(valueBits, positions);
                var actual = isLeft ? Bits.ShiftLeft(value, positions) : Bits.ShiftRight(value, positions);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"Shift{(isLeft ? "Left" : "Right")}({value}, {positions})");
            }

            string ShiftString(string bits, int positions)
            {
                var actualPositions = positions % bits.Length;
                if (actualPositions < 0) { actualPositions += bits.Length; }
                return isLeft 
                    ? bits.Substring(actualPositions, bits.Length - actualPositions) + new string('0', actualPositions)
                    : new string(CodeGenerator.IsUnsigned(typeof(int)) ? '0' : bits[0], actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

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
        /// <see cref="Bits.BitCount(int)"/>
        /// </summary>
        [Test]
        public void TestBitCountInt32()
        {
            Assert.AreEqual(0, Bits.BitCount((int)0));
            Assert.AreEqual(1, Bits.BitCount((int)1));
            Assert.AreEqual(2, Bits.BitCount(default(int).SetBit(Bits.SizeOfInt32InBits / 2).SetBit(Bits.SizeOfInt32InBits - 1)));

            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);
            Assert.AreEqual(Bits.SizeOfInt32InBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfInt32InBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfInt32InBits / 2).ClearBit(0)));

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt32();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.TrailingZeroBitCount(int)"/>
        /// </summary>
        [Test]
        public void TestTrailingZeroBitCountInt32()
        {
            Assert.AreEqual(Bits.SizeOfInt32InBits, Bits.TrailingZeroBitCount((int)0));
            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);
            Assert.AreEqual(0, Bits.TrailingZeroBitCount(allBitsSet));

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(i + 1, Bits.TrailingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt32();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfInt32InBits : binaryString.Length - binaryString.TrimEnd('0').Length, Bits.TrailingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.LeadingZeroBitCount(int)"/>
        /// </summary>
        [Test]
        public void TestLeadingZeroBitCountInt32()
        {
            Assert.AreEqual(Bits.SizeOfInt32InBits, Bits.LeadingZeroBitCount((int)0));
            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);
            Assert.AreEqual(0, Bits.LeadingZeroBitCount(allBitsSet));

            for (var i = Bits.SizeOfInt32InBits - 1; i >= 0; --i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(Bits.SizeOfInt32InBits - i, Bits.LeadingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt32();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfInt32InBits : Bits.SizeOfInt32InBits - binaryString.Length, Bits.LeadingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.RotateLeft(int, int)"/>
        /// </summary>
        [Test]
        public void TestRotateLeftInt32() => this.TestRotateInt32Helper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.RotateRight(int, int)"/>
        /// </summary>
        [Test]
        public void TestRotateRightInt32() => this.TestRotateInt32Helper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestRotateLeftInt32"/> and <see cref="TestRotateRightInt32"/>
        /// </summary>
        private void TestRotateInt32Helper(bool isLeft)
        {
            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfInt32InBits; i < Bits.SizeOfInt32InBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt32();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(int value, int positions)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = RotateString(valueBits, positions);
                var actual = isLeft ? Bits.RotateLeft(value, positions) : Bits.RotateRight(value, positions);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"Rotate{(isLeft ? "Left" : "Right")}({value}, {positions})");
            }

            string RotateString(string bits, int positions)
            {
                var actualPositions = positions % bits.Length;
                if (actualPositions < 0) { actualPositions += Bits.SizeOfInt32InBits; }
                return isLeft
                    ? bits.Substring(actualPositions) + bits.Substring(0, actualPositions)
                    : bits.Substring(bits.Length - actualPositions) + bits.Substring(0, bits.Length - actualPositions);
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

        /// <summary>
        /// Helper buffer for <see cref="GetRandomInt32"/>
        /// </summary>
        private readonly byte[] _getRandomBufferInt32 = new byte[sizeof(int)];

        /// <summary>
        /// Helper to generate random <see cref="int"/> values for fuzz testing
        /// </summary>
        private int GetRandomInt32()
        {
            this._random.Value.NextBytes(this._getRandomBufferInt32);
            int value = 0;
            for (var i = 0; i < sizeof(int); ++i)
            {
                value = unchecked((int)((int)(value << 8) & (int)this._getRandomBufferInt32[i]));
            }
            return value;
        }

        
    }
}
