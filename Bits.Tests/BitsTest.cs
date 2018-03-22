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
        /// <see cref="Bits.ShiftLeft(long, int)"/>
        /// </summary>
        [Test]
        public void TestShiftLeftInt64() => this.TestShiftInt64Helper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.ShiftRight(long, int)"/>
        /// </summary>
        [Test]
        public void TestShiftRightInt64() => this.TestShiftInt64Helper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestShiftLeftInt64"/> and <see cref="TestShiftRightInt64"/>
        /// </summary>
        private void TestShiftInt64Helper(bool isLeft)
        {
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfInt64InBits; i < Bits.SizeOfInt64InBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt64();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(long value, int positions)
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
                    : new string(CodeGenerator.IsUnsigned(typeof(long)) ? '0' : bits[0], actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.And(long, long)"/>
        /// </summary>
        [Test]
        public void TestAndInt64()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomInt64();
                var randomValue2 = this.GetRandomInt64();
                Assert.AreEqual((long)(randomValue1 & randomValue2), Bits.And(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Or(long, long)"/>
        /// </summary>
        [Test]
        public void TestOrInt64()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomInt64();
                var randomValue2 = this.GetRandomInt64();
                Assert.AreEqual((long)(randomValue1 | randomValue2), Bits.Or(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Not(long)"/>
        /// </summary>
        [Test]
        public void TestNotInt64()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt64();
                Assert.AreEqual((long)(~randomValue), Bits.Not(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.Xor(long, long)"/>
        /// </summary>
        [Test]
        public void TestXorInt64()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomInt64();
                var randomValue2 = this.GetRandomInt64();
                Assert.AreEqual((long)(randomValue1 ^ randomValue2), Bits.Xor(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.HasAnyFlag(long, long)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagInt64()
        {
            Assert.IsTrue(((long)1).HasAnyFlag(1));
            Assert.IsTrue(((long)3).HasAnyFlag(2));
            Assert.IsFalse(((long)18).HasAnyFlag(9));
            Assert.IsFalse(long.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((long)0).HasAnyFlag(0));
        }

        /// <summary>
        /// <see cref="Bits.HasAllFlags(long, long)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsInt64()
        {
            Assert.IsTrue(((long)1).HasAllFlags(1));
            Assert.IsTrue(((long)3).HasAllFlags(2));
            Assert.IsFalse(((long)18).HasAllFlags(9));
            Assert.IsTrue(long.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((long)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(long, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitInt64()
        {
            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Assert.IsTrue((((long)1) << i).GetBit(i));
                Assert.IsFalse((~(((long)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(long).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(long).GetBit(Bits.SizeOfInt64InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.SetBit(long, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitInt64()
        {
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);
            var noBitsSet = default(long);

            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Assert.AreEqual((long)(((long)1) << i), default(long).SetBit(i));
                Assert.AreEqual(((long)25).SetBit(i), ((long)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
                noBitsSet = noBitsSet.SetBit(i);
            }

            Assert.AreEqual(allBitsSet, noBitsSet);

            Assert.Throws<IndexOutOfRangeException>(() => default(long).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(long).SetBit(Bits.SizeOfInt64InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearBit(long, int)"/>
        /// </summary>
        [Test]
        public void TestClearBitInt64()
        {
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);

            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Assert.AreEqual((long)0, default(long).ClearBit(i));
                Assert.AreEqual(((long)25).ClearBit(i), ((long)25).ClearBit(i).ClearBit(i));
                Assert.AreEqual((long)~(((long)1) << i), allBitsSet.ClearBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(long).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(long).SetBit(Bits.SizeOfInt64InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(long, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitInt64()
        {
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);

            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Assert.AreEqual((long)(((long)1) << i), default(long).FlipBit(i));
                Assert.AreEqual(((long)25), ((long)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((long)~(((long)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(long).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(long).SetBit(Bits.SizeOfInt64InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearLeastSignificantSetBit(long)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantSetBitInt64()
        {
            Assert.AreEqual((long)0b101000, Bits.ClearLeastSignificantSetBit((long)0b101100));
            Assert.AreEqual(default(long), Bits.ClearLeastSignificantSetBit(default(long)));

            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);

            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Assert.AreEqual(default(long), Bits.ClearLeastSignificantSetBit((long)((long)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantSetBit(allBitsSet);
            }

            Assert.AreEqual(default(long), allBitsSet);
        }

        /// <summary>
        /// <see cref="Bits.SetLeastSignificantZeroBit(long)(long)"/>
        /// </summary>
        [Test]
        public void TestSetLeastSignificantZeroBitInt64()
        {
            Assert.AreEqual((long)0b010111, Bits.SetLeastSignificantZeroBit((long)0b010011));
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);
            Assert.AreEqual(allBitsSet, Bits.SetLeastSignificantZeroBit(allBitsSet));

            var value = default(long);
            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Assert.AreEqual(allBitsSet, Bits.SetLeastSignificantZeroBit(allBitsSet.ClearBit(i)));
                value = Bits.SetLeastSignificantZeroBit(allBitsSet);
            }

            Assert.AreEqual(allBitsSet, value);
        }

        /// <summary>
        /// <see cref="Bits.SetTrailingZeroBits(long)"/>
        /// </summary>
        [Test]
        public void TestSetTrailingZeroBitsInt64()
        {
            Check(default(long));
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);
            Check(allBitsSet);
            Check(long.MinValue);
            Check(long.MaxValue);

            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Check(Bits.ShiftLeft((long)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomInt64());
            }

            void Check(long value)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = SetTrailingZeroBitsString(valueBits);
                var actual = Bits.SetTrailingZeroBits(value);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"TestSetTrailingZeroBitsInt64({value} /* {valueBits} */)");
            }

            string SetTrailingZeroBitsString(string bits)
            {
                var lastOneBitIndex = bits.LastIndexOf('1');
                return bits.Substring(0, lastOneBitIndex + 1) + new string('1', bits.Length - (lastOneBitIndex + 1));
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantSetBit(long)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantSetBitInt64()
        {
            Assert.AreEqual((long)0b000100, Bits.IsolateLeastSignificantSetBit((long)0b101100));
            Assert.AreEqual(default(long), Bits.IsolateLeastSignificantSetBit(default(long)));

            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Assert.AreEqual(default(long).SetBit(i), Bits.IsolateLeastSignificantSetBit((default(long).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(long).SetBit(i - 1), Bits.IsolateLeastSignificantSetBit(default(long).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantSetBit(long)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantSetBitInt64()
        {
            Assert.AreEqual((long)0b100000, Bits.IsolateMostSignificantSetBit((long)0b101100));
            Assert.AreEqual(default(long), Bits.IsolateMostSignificantSetBit(default(long)));

            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Assert.AreEqual(default(long).SetBit(i), Bits.IsolateMostSignificantSetBit((default(long).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(long).SetBit(i), Bits.IsolateMostSignificantSetBit(default(long).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.BitCount(long)"/>
        /// </summary>
        [Test]
        public void TestBitCountInt64()
        {
            Assert.AreEqual(0, Bits.BitCount((long)0));
            Assert.AreEqual(1, Bits.BitCount((long)1));
            Assert.AreEqual(2, Bits.BitCount(default(long).SetBit(Bits.SizeOfInt64InBits / 2).SetBit(Bits.SizeOfInt64InBits - 1)));

            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);
            Assert.AreEqual(Bits.SizeOfInt64InBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfInt64InBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfInt64InBits / 2).ClearBit(0)));

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt64();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.TrailingZeroBitCount(long)"/>
        /// </summary>
        [Test]
        public void TestTrailingZeroBitCountInt64()
        {
            Assert.AreEqual(Bits.SizeOfInt64InBits, Bits.TrailingZeroBitCount((long)0));
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);
            Assert.AreEqual(0, Bits.TrailingZeroBitCount(allBitsSet));

            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(i + 1, Bits.TrailingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt64();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfInt64InBits : binaryString.Length - binaryString.TrimEnd('0').Length, Bits.TrailingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.LeadingZeroBitCount(long)"/>
        /// </summary>
        [Test]
        public void TestLeadingZeroBitCountInt64()
        {
            Assert.AreEqual(Bits.SizeOfInt64InBits, Bits.LeadingZeroBitCount((long)0));
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);
            Assert.AreEqual(0, Bits.LeadingZeroBitCount(allBitsSet));

            for (var i = Bits.SizeOfInt64InBits - 1; i >= 0; --i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(Bits.SizeOfInt64InBits - i, Bits.LeadingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt64();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfInt64InBits : Bits.SizeOfInt64InBits - binaryString.Length, Bits.LeadingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.RotateLeft(long, int)"/>
        /// </summary>
        [Test]
        public void TestRotateLeftInt64() => this.TestRotateInt64Helper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.RotateRight(long, int)"/>
        /// </summary>
        [Test]
        public void TestRotateRightInt64() => this.TestRotateInt64Helper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestRotateLeftInt64"/> and <see cref="TestRotateRightInt64"/>
        /// </summary>
        private void TestRotateInt64Helper(bool isLeft)
        {
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfInt64InBits; i < Bits.SizeOfInt64InBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt64();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(long value, int positions)
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
                if (actualPositions < 0) { actualPositions += Bits.SizeOfInt64InBits; }
                return isLeft
                    ? bits.Substring(actualPositions) + bits.Substring(0, actualPositions)
                    : bits.Substring(bits.Length - actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.Reverse(long)"/>
        /// </summary>
        [Test]
        public void TestReverseInt64()
        {
            Check(default(long));
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);
            Check(allBitsSet);
            Check(long.MinValue);
            Check(long.MaxValue);

            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Check(Bits.ShiftLeft((long)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomInt64());
            }

            void Check(long value)
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
        /// <see cref="Bits.ReverseBytes(long)"/>
        /// </summary>
        [Test]
        public void TestReverseBytesInt64()
        {
            Check(default(long));
            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);
            Check(allBitsSet);
            Check(long.MinValue);
            Check(long.MaxValue);

            for (var i = 0; i < Bits.SizeOfInt64InBits; ++i)
            {
                Check(Bits.ShiftLeft((long)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomInt64());
            }

            void Check(long value)
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
        /// <see cref="Bits.ToShortBinaryString(long)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringInt64()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((long)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((long)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((long)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((long)0b01010101)));

            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfInt64InBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(long)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringInt64()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfInt64InBits), Bits.ToLongBinaryString((long)0));
            Assert.AreEqual(new string('0', Bits.SizeOfInt64InBits - 1) + "1", Bits.ToLongBinaryString((long)1));
            Assert.AreEqual(new string('0', Bits.SizeOfInt64InBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((long)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfInt64InBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((long)0b01010101)));

            var allBitsSet = long.MinValue == default(long) ? long.MaxValue : unchecked((long)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfInt64InBits), Bits.ToLongBinaryString(allBitsSet));
        }

        /// <summary>
        /// Helper buffer for <see cref="GetRandomInt64"/>
        /// </summary>
        private readonly byte[] _getRandomBufferInt64 = new byte[sizeof(long)];

        /// <summary>
        /// Helper to generate random <see cref="long"/> values for fuzz testing
        /// </summary>
        private long GetRandomInt64()
        {
            this._random.Value.NextBytes(this._getRandomBufferInt64);
            long value = 0;
            for (var i = 0; i < sizeof(long); ++i)
            {
                value = unchecked((long)((long)(value << 8) & (long)this._getRandomBufferInt64[i]));
            }
            return value;
        }

        // END MEMBERS
    }
}
