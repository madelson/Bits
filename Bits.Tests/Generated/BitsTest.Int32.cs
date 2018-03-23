//
// AUTO-GENERATED
//
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Medallion.Tests
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
        /// <see cref="Bits.And(int, int)"/>
        /// </summary>
        [Test]
        public void TestAndInt32()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomInt32();
                var randomValue2 = this.GetRandomInt32();
                Assert.AreEqual((int)(randomValue1 & randomValue2), Bits.And(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Or(int, int)"/>
        /// </summary>
        [Test]
        public void TestOrInt32()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomInt32();
                var randomValue2 = this.GetRandomInt32();
                Assert.AreEqual((int)(randomValue1 | randomValue2), Bits.Or(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Not(int)"/>
        /// </summary>
        [Test]
        public void TestNotInt32()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt32();
                Assert.AreEqual((int)(~randomValue), Bits.Not(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.Xor(int, int)"/>
        /// </summary>
        [Test]
        public void TestXorInt32()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomInt32();
                var randomValue2 = this.GetRandomInt32();
                Assert.AreEqual((int)(randomValue1 ^ randomValue2), Bits.Xor(randomValue1, randomValue2));
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
        /// <see cref="Bits.ClearLeastSignificantOneBit(int)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantOneBitInt32()
        {
            Assert.AreEqual((int)0b101000, Bits.ClearLeastSignificantOneBit((int)0b101100));
            Assert.AreEqual(default(int), Bits.ClearLeastSignificantOneBit(default(int)));

            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.AreEqual(default(int), Bits.ClearLeastSignificantOneBit((int)((int)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantOneBit(allBitsSet);
            }

            Assert.AreEqual(default(int), allBitsSet);
        }

        /// <summary>
        /// <see cref="Bits.SetLeastSignificantZeroBit(int)(int)"/>
        /// </summary>
        [Test]
        public void TestSetLeastSignificantZeroBitInt32()
        {
            Assert.AreEqual((int)0b010111, Bits.SetLeastSignificantZeroBit((int)0b010011));
            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);
            Assert.AreEqual(allBitsSet, Bits.SetLeastSignificantZeroBit(allBitsSet));

            var value = default(int);
            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.AreEqual(allBitsSet, Bits.SetLeastSignificantZeroBit(allBitsSet.ClearBit(i)));
                value = Bits.SetLeastSignificantZeroBit(allBitsSet);
            }

            Assert.AreEqual(allBitsSet, value);
        }

        /// <summary>
        /// <see cref="Bits.SetTrailingZeroBits(int)"/>
        /// </summary>
        [Test]
        public void TestSetTrailingZeroBitsInt32()
        {
            Check(default(int));
            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);
            Check(allBitsSet);
            Check(int.MinValue);
            Check(int.MaxValue);

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Check(Bits.ShiftLeft((int)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomInt32());
            }

            void Check(int value)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = SetTrailingZeroBitsString(valueBits);
                var actual = Bits.SetTrailingZeroBits(value);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"TestSetTrailingZeroBitsInt32({value} /* {valueBits} */)");
            }

            string SetTrailingZeroBitsString(string bits)
            {
                var lastOneBitIndex = bits.LastIndexOf('1');
                return bits.Substring(0, lastOneBitIndex + 1) + new string('1', bits.Length - (lastOneBitIndex + 1));
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantOneBit(int)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantOneBitInt32()
        {
            Assert.AreEqual((int)0b000100, Bits.IsolateLeastSignificantOneBit((int)0b101100));
            Assert.AreEqual(default(int), Bits.IsolateLeastSignificantOneBit(default(int)));

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.AreEqual(default(int).SetBit(i), Bits.IsolateLeastSignificantOneBit((default(int).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(int).SetBit(i - 1), Bits.IsolateLeastSignificantOneBit(default(int).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantOneBit(int)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantOneBitInt32()
        {
            Assert.AreEqual((int)0b100000, Bits.IsolateMostSignificantOneBit((int)0b101100));
            Assert.AreEqual(default(int), Bits.IsolateMostSignificantOneBit(default(int)));

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.AreEqual(default(int).SetBit(i), Bits.IsolateMostSignificantOneBit((default(int).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(int).SetBit(i), Bits.IsolateMostSignificantOneBit(default(int).SetBit(i - 1).SetBit(i)));
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
        /// <see cref="Bits.HasSingleOneBit(int)"/>
        /// </summary>
        [Test]
        public void TestHasSingleOneBitInt32()
        {
            Assert.IsTrue(Bits.HasSingleOneBit((int)1));
            Assert.IsFalse(Bits.HasSingleOneBit((int)0));
            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);
            Assert.IsFalse(Bits.HasSingleOneBit(allBitsSet));

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Assert.IsTrue(Bits.HasSingleOneBit(Bits.ShiftLeft((int)1, i)));
                Assert.IsFalse(Bits.HasSingleOneBit(Bits.Not(Bits.ShiftLeft((int)1, i))));
                if (i < Bits.SizeOfInt32InBits - 1)
                {
                    Assert.IsFalse(Bits.HasSingleOneBit(Bits.ShiftLeft((int)0b11, i)));
                }
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
        /// <see cref="Bits.Reverse(int)"/>
        /// </summary>
        [Test]
        public void TestReverseInt32()
        {
            Check(default(int));
            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);
            Check(allBitsSet);
            Check(int.MinValue);
            Check(int.MaxValue);

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Check(Bits.ShiftLeft((int)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomInt32());
            }

            void Check(int value)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = ReverseString(valueBits);
                var actual = Bits.Reverse(value);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"Reverse({value} /* {valueBits} */)");
            }

            string ReverseString(string bits)
            {
                var array = bits.ToCharArray();
                Array.Reverse(array);
                return new string(array);
            }
        }

        /// <summary>
        /// <see cref="Bits.ReverseBytes(int)"/>
        /// </summary>
        [Test]
        public void TestReverseBytesInt32()
        {
            Check(default(int));
            var allBitsSet = int.MinValue == default(int) ? int.MaxValue : unchecked((int)-1);
            Check(allBitsSet);
            Check(int.MinValue);
            Check(int.MaxValue);

            for (var i = 0; i < Bits.SizeOfInt32InBits; ++i)
            {
                Check(Bits.ShiftLeft((int)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomInt32());
            }

            void Check(int value)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = ReverseStringBytes(valueBits);
                var actual = Bits.ReverseBytes(value);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"Reverse({value} /* {valueBits} */)");
            }

            string ReverseStringBytes(string bits)
            {
                return string.Join(
                    string.Empty,
                    Enumerable.Range(0, bits.Length / 8)
                        .Select(i => bits.Substring(i * 8, 8))
                        .Reverse()
                );
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
