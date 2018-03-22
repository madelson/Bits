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
        /// <see cref="Bits.ShiftLeft(uint, int)"/>
        /// </summary>
        [Test]
        public void TestShiftLeftUInt32() => this.TestShiftUInt32Helper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.ShiftRight(uint, int)"/>
        /// </summary>
        [Test]
        public void TestShiftRightUInt32() => this.TestShiftUInt32Helper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestShiftLeftUInt32"/> and <see cref="TestShiftRightUInt32"/>
        /// </summary>
        private void TestShiftUInt32Helper(bool isLeft)
        {
            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfUInt32InBits; i < Bits.SizeOfUInt32InBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt32();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(uint value, int positions)
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
                    : new string(CodeGenerator.IsUnsigned(typeof(uint)) ? '0' : bits[0], actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.And(uint, uint)"/>
        /// </summary>
        [Test]
        public void TestAndUInt32()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomUInt32();
                var randomValue2 = this.GetRandomUInt32();
                Assert.AreEqual((uint)(randomValue1 & randomValue2), Bits.And(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Or(uint, uint)"/>
        /// </summary>
        [Test]
        public void TestOrUInt32()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomUInt32();
                var randomValue2 = this.GetRandomUInt32();
                Assert.AreEqual((uint)(randomValue1 | randomValue2), Bits.Or(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Not(uint)"/>
        /// </summary>
        [Test]
        public void TestNotUInt32()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt32();
                Assert.AreEqual((uint)(~randomValue), Bits.Not(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.Xor(uint, uint)"/>
        /// </summary>
        [Test]
        public void TestXorUInt32()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomUInt32();
                var randomValue2 = this.GetRandomUInt32();
                Assert.AreEqual((uint)(randomValue1 ^ randomValue2), Bits.Xor(randomValue1, randomValue2));
            }
        }

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

        /// <summary>
        /// <see cref="Bits.BitCount(uint)"/>
        /// </summary>
        [Test]
        public void TestBitCountUInt32()
        {
            Assert.AreEqual(0, Bits.BitCount((uint)0));
            Assert.AreEqual(1, Bits.BitCount((uint)1));
            Assert.AreEqual(2, Bits.BitCount(default(uint).SetBit(Bits.SizeOfUInt32InBits / 2).SetBit(Bits.SizeOfUInt32InBits - 1)));

            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);
            Assert.AreEqual(Bits.SizeOfUInt32InBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfUInt32InBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfUInt32InBits / 2).ClearBit(0)));

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt32();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.TrailingZeroBitCount(uint)"/>
        /// </summary>
        [Test]
        public void TestTrailingZeroBitCountUInt32()
        {
            Assert.AreEqual(Bits.SizeOfUInt32InBits, Bits.TrailingZeroBitCount((uint)0));
            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);
            Assert.AreEqual(0, Bits.TrailingZeroBitCount(allBitsSet));

            for (var i = 0; i < Bits.SizeOfUInt32InBits; ++i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(i + 1, Bits.TrailingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt32();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfUInt32InBits : binaryString.Length - binaryString.TrimEnd('0').Length, Bits.TrailingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.LeadingZeroBitCount(uint)"/>
        /// </summary>
        [Test]
        public void TestLeadingZeroBitCountUInt32()
        {
            Assert.AreEqual(Bits.SizeOfUInt32InBits, Bits.LeadingZeroBitCount((uint)0));
            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);
            Assert.AreEqual(0, Bits.LeadingZeroBitCount(allBitsSet));

            for (var i = Bits.SizeOfUInt32InBits - 1; i >= 0; --i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(Bits.SizeOfUInt32InBits - i, Bits.LeadingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt32();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfUInt32InBits : Bits.SizeOfUInt32InBits - binaryString.Length, Bits.LeadingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.RotateLeft(uint, int)"/>
        /// </summary>
        [Test]
        public void TestRotateLeftUInt32() => this.TestRotateUInt32Helper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.RotateRight(uint, int)"/>
        /// </summary>
        [Test]
        public void TestRotateRightUInt32() => this.TestRotateUInt32Helper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestRotateLeftUInt32"/> and <see cref="TestRotateRightUInt32"/>
        /// </summary>
        private void TestRotateUInt32Helper(bool isLeft)
        {
            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfUInt32InBits; i < Bits.SizeOfUInt32InBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt32();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(uint value, int positions)
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
                if (actualPositions < 0) { actualPositions += Bits.SizeOfUInt32InBits; }
                return isLeft
                    ? bits.Substring(actualPositions) + bits.Substring(0, actualPositions)
                    : bits.Substring(bits.Length - actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.Reverse(uint)"/>
        /// </summary>
        [Test]
        public void TestReverseUInt32()
        {
            Check(default(uint));
            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);
            Check(allBitsSet);
            Check(uint.MinValue);
            Check(uint.MaxValue);

            for (var i = 0; i < Bits.SizeOfUInt32InBits; ++i)
            {
                Check(Bits.ShiftLeft((uint)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomUInt32());
            }

            void Check(uint value)
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
        /// <see cref="Bits.ReverseBytes(uint)"/>
        /// </summary>
        [Test]
        public void TestReverseBytesUInt32()
        {
            Check(default(uint));
            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);
            Check(allBitsSet);
            Check(uint.MinValue);
            Check(uint.MaxValue);

            for (var i = 0; i < Bits.SizeOfUInt32InBits; ++i)
            {
                Check(Bits.ShiftLeft((uint)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomUInt32());
            }

            void Check(uint value)
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
        /// <see cref="Bits.ToShortBinaryString(uint)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringUInt32()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((uint)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((uint)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((uint)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((uint)0b01010101)));

            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfUInt32InBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(uint)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringUInt32()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfUInt32InBits), Bits.ToLongBinaryString((uint)0));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt32InBits - 1) + "1", Bits.ToLongBinaryString((uint)1));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt32InBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((uint)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt32InBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((uint)0b01010101)));

            var allBitsSet = uint.MinValue == default(uint) ? uint.MaxValue : unchecked((uint)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfUInt32InBits), Bits.ToLongBinaryString(allBitsSet));
        }

        /// <summary>
        /// Helper buffer for <see cref="GetRandomUInt32"/>
        /// </summary>
        private readonly byte[] _getRandomBufferUInt32 = new byte[sizeof(uint)];

        /// <summary>
        /// Helper to generate random <see cref="uint"/> values for fuzz testing
        /// </summary>
        private uint GetRandomUInt32()
        {
            this._random.Value.NextBytes(this._getRandomBufferUInt32);
            uint value = 0;
            for (var i = 0; i < sizeof(uint); ++i)
            {
                value = unchecked((uint)((uint)(value << 8) & (uint)this._getRandomBufferUInt32[i]));
            }
            return value;
        }

        
    }
}
