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
        /// <see cref="Bits.ShiftLeft(ulong, int)"/>
        /// </summary>
        [Test]
        public void TestShiftLeftUInt64() => this.TestShiftUInt64Helper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.ShiftRight(ulong, int)"/>
        /// </summary>
        [Test]
        public void TestShiftRightUInt64() => this.TestShiftUInt64Helper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestShiftLeftUInt64"/> and <see cref="TestShiftRightUInt64"/>
        /// </summary>
        private void TestShiftUInt64Helper(bool isLeft)
        {
            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfUInt64InBits; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt64();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(ulong value, int positions)
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
                    : new string(CodeGenerator.IsUnsigned(typeof(ulong)) ? '0' : bits[0], actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.And(ulong, ulong)"/>
        /// </summary>
        [Test]
        public void TestAndUInt64()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomUInt64();
                var randomValue2 = this.GetRandomUInt64();
                Assert.AreEqual((ulong)(randomValue1 & randomValue2), Bits.And(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Or(ulong, ulong)"/>
        /// </summary>
        [Test]
        public void TestOrUInt64()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomUInt64();
                var randomValue2 = this.GetRandomUInt64();
                Assert.AreEqual((ulong)(randomValue1 | randomValue2), Bits.Or(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Not(ulong)"/>
        /// </summary>
        [Test]
        public void TestNotUInt64()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt64();
                Assert.AreEqual((ulong)(~randomValue), Bits.Not(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.Xor(ulong, ulong)"/>
        /// </summary>
        [Test]
        public void TestXorUInt64()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomUInt64();
                var randomValue2 = this.GetRandomUInt64();
                Assert.AreEqual((ulong)(randomValue1 ^ randomValue2), Bits.Xor(randomValue1, randomValue2));
            }
        }

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
        /// <see cref="Bits.SetTrailingZeroBits(ulong)"/>
        /// </summary>
        [Test]
        public void TestSetTrailingZeroBitsUInt64()
        {
            Check(default(ulong));
            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            Check(allBitsSet);
            Check(ulong.MinValue);
            Check(ulong.MaxValue);

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Check(Bits.ShiftLeft((ulong)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomUInt64());
            }

            void Check(ulong value)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = SetTrailingZeroBitsString(valueBits);
                var actual = Bits.SetTrailingZeroBits(value);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"TestSetTrailingZeroBitsUInt64({value} /* {valueBits} */)");
            }

            string SetTrailingZeroBitsString(string bits)
            {
                var lastOneBitIndex = bits.LastIndexOf('1');
                return bits.Substring(0, lastOneBitIndex + 1) + new string('1', bits.Length - (lastOneBitIndex + 1));
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantSetBit(ulong)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantSetBitUInt64()
        {
            Assert.AreEqual((ulong)0b000100, Bits.IsolateLeastSignificantSetBit((ulong)0b101100));
            Assert.AreEqual(default(ulong), Bits.IsolateLeastSignificantSetBit(default(ulong)));

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Assert.AreEqual(default(ulong).SetBit(i), Bits.IsolateLeastSignificantSetBit((default(ulong).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(ulong).SetBit(i - 1), Bits.IsolateLeastSignificantSetBit(default(ulong).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantSetBit(ulong)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantSetBitUInt64()
        {
            Assert.AreEqual((ulong)0b100000, Bits.IsolateMostSignificantSetBit((ulong)0b101100));
            Assert.AreEqual(default(ulong), Bits.IsolateMostSignificantSetBit(default(ulong)));

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Assert.AreEqual(default(ulong).SetBit(i), Bits.IsolateMostSignificantSetBit((default(ulong).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(ulong).SetBit(i), Bits.IsolateMostSignificantSetBit(default(ulong).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.BitCount(ulong)"/>
        /// </summary>
        [Test]
        public void TestBitCountUInt64()
        {
            Assert.AreEqual(0, Bits.BitCount((ulong)0));
            Assert.AreEqual(1, Bits.BitCount((ulong)1));
            Assert.AreEqual(2, Bits.BitCount(default(ulong).SetBit(Bits.SizeOfUInt64InBits / 2).SetBit(Bits.SizeOfUInt64InBits - 1)));

            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            Assert.AreEqual(Bits.SizeOfUInt64InBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfUInt64InBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfUInt64InBits / 2).ClearBit(0)));

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt64();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.TrailingZeroBitCount(ulong)"/>
        /// </summary>
        [Test]
        public void TestTrailingZeroBitCountUInt64()
        {
            Assert.AreEqual(Bits.SizeOfUInt64InBits, Bits.TrailingZeroBitCount((ulong)0));
            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            Assert.AreEqual(0, Bits.TrailingZeroBitCount(allBitsSet));

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(i + 1, Bits.TrailingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt64();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfUInt64InBits : binaryString.Length - binaryString.TrimEnd('0').Length, Bits.TrailingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.LeadingZeroBitCount(ulong)"/>
        /// </summary>
        [Test]
        public void TestLeadingZeroBitCountUInt64()
        {
            Assert.AreEqual(Bits.SizeOfUInt64InBits, Bits.LeadingZeroBitCount((ulong)0));
            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            Assert.AreEqual(0, Bits.LeadingZeroBitCount(allBitsSet));

            for (var i = Bits.SizeOfUInt64InBits - 1; i >= 0; --i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(Bits.SizeOfUInt64InBits - i, Bits.LeadingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt64();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfUInt64InBits : Bits.SizeOfUInt64InBits - binaryString.Length, Bits.LeadingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.RotateLeft(ulong, int)"/>
        /// </summary>
        [Test]
        public void TestRotateLeftUInt64() => this.TestRotateUInt64Helper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.RotateRight(ulong, int)"/>
        /// </summary>
        [Test]
        public void TestRotateRightUInt64() => this.TestRotateUInt64Helper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestRotateLeftUInt64"/> and <see cref="TestRotateRightUInt64"/>
        /// </summary>
        private void TestRotateUInt64Helper(bool isLeft)
        {
            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfUInt64InBits; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt64();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(ulong value, int positions)
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
                if (actualPositions < 0) { actualPositions += Bits.SizeOfUInt64InBits; }
                return isLeft
                    ? bits.Substring(actualPositions) + bits.Substring(0, actualPositions)
                    : bits.Substring(bits.Length - actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.Reverse(ulong)"/>
        /// </summary>
        [Test]
        public void TestReverseUInt64()
        {
            Check(default(ulong));
            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            Check(allBitsSet);
            Check(ulong.MinValue);
            Check(ulong.MaxValue);

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Check(Bits.ShiftLeft((ulong)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomUInt64());
            }

            void Check(ulong value)
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
        /// <see cref="Bits.ReverseBytes(ulong)"/>
        /// </summary>
        [Test]
        public void TestReverseBytesUInt64()
        {
            Check(default(ulong));
            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            Check(allBitsSet);
            Check(ulong.MinValue);
            Check(ulong.MaxValue);

            for (var i = 0; i < Bits.SizeOfUInt64InBits; ++i)
            {
                Check(Bits.ShiftLeft((ulong)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomUInt64());
            }

            void Check(ulong value)
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
        /// <see cref="Bits.ToShortBinaryString(ulong)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringUInt64()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((ulong)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((ulong)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((ulong)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((ulong)0b01010101)));

            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfUInt64InBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(ulong)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringUInt64()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfUInt64InBits), Bits.ToLongBinaryString((ulong)0));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt64InBits - 1) + "1", Bits.ToLongBinaryString((ulong)1));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt64InBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((ulong)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt64InBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((ulong)0b01010101)));

            var allBitsSet = ulong.MinValue == default(ulong) ? ulong.MaxValue : unchecked((ulong)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfUInt64InBits), Bits.ToLongBinaryString(allBitsSet));
        }

        /// <summary>
        /// Helper buffer for <see cref="GetRandomUInt64"/>
        /// </summary>
        private readonly byte[] _getRandomBufferUInt64 = new byte[sizeof(ulong)];

        /// <summary>
        /// Helper to generate random <see cref="ulong"/> values for fuzz testing
        /// </summary>
        private ulong GetRandomUInt64()
        {
            this._random.Value.NextBytes(this._getRandomBufferUInt64);
            ulong value = 0;
            for (var i = 0; i < sizeof(ulong); ++i)
            {
                value = unchecked((ulong)((ulong)(value << 8) & (ulong)this._getRandomBufferUInt64[i]));
            }
            return value;
        }

        
    }
}
