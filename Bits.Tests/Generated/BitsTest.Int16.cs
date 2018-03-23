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
        /// <see cref="Bits.ShiftLeft(short, int)"/>
        /// </summary>
        [Test]
        public void TestShiftLeftInt16() => this.TestShiftInt16Helper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.ShiftRight(short, int)"/>
        /// </summary>
        [Test]
        public void TestShiftRightInt16() => this.TestShiftInt16Helper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestShiftLeftInt16"/> and <see cref="TestShiftRightInt16"/>
        /// </summary>
        private void TestShiftInt16Helper(bool isLeft)
        {
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfInt16InBits; i < Bits.SizeOfInt16InBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt16();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(short value, int positions)
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
                    : new string(CodeGenerator.IsUnsigned(typeof(short)) ? '0' : bits[0], actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.And(short, short)"/>
        /// </summary>
        [Test]
        public void TestAndInt16()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomInt16();
                var randomValue2 = this.GetRandomInt16();
                Assert.AreEqual((short)(randomValue1 & randomValue2), Bits.And(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Or(short, short)"/>
        /// </summary>
        [Test]
        public void TestOrInt16()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomInt16();
                var randomValue2 = this.GetRandomInt16();
                Assert.AreEqual((short)(randomValue1 | randomValue2), Bits.Or(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Not(short)"/>
        /// </summary>
        [Test]
        public void TestNotInt16()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt16();
                Assert.AreEqual((short)(~randomValue), Bits.Not(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.Xor(short, short)"/>
        /// </summary>
        [Test]
        public void TestXorInt16()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomInt16();
                var randomValue2 = this.GetRandomInt16();
                Assert.AreEqual((short)(randomValue1 ^ randomValue2), Bits.Xor(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.HasAnyFlag(short, short)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagInt16()
        {
            Assert.IsTrue(((short)1).HasAnyFlag(1));
            Assert.IsTrue(((short)3).HasAnyFlag(2));
            Assert.IsFalse(((short)18).HasAnyFlag(9));
            Assert.IsFalse(short.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((short)0).HasAnyFlag(0));
        }

        /// <summary>
        /// <see cref="Bits.HasAllFlags(short, short)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsInt16()
        {
            Assert.IsTrue(((short)1).HasAllFlags(1));
            Assert.IsTrue(((short)3).HasAllFlags(2));
            Assert.IsFalse(((short)18).HasAllFlags(9));
            Assert.IsTrue(short.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((short)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(short, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitInt16()
        {
            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.IsTrue((((short)1) << i).GetBit(i));
                Assert.IsFalse((~(((short)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(short).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(short).GetBit(Bits.SizeOfInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.SetBit(short, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitInt16()
        {
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            var noBitsSet = default(short);

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual((short)(((short)1) << i), default(short).SetBit(i));
                Assert.AreEqual(((short)25).SetBit(i), ((short)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
                noBitsSet = noBitsSet.SetBit(i);
            }

            Assert.AreEqual(allBitsSet, noBitsSet);

            Assert.Throws<IndexOutOfRangeException>(() => default(short).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(short).SetBit(Bits.SizeOfInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearBit(short, int)"/>
        /// </summary>
        [Test]
        public void TestClearBitInt16()
        {
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual((short)0, default(short).ClearBit(i));
                Assert.AreEqual(((short)25).ClearBit(i), ((short)25).ClearBit(i).ClearBit(i));
                Assert.AreEqual((short)~(((short)1) << i), allBitsSet.ClearBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(short).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(short).SetBit(Bits.SizeOfInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(short, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitInt16()
        {
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual((short)(((short)1) << i), default(short).FlipBit(i));
                Assert.AreEqual(((short)25), ((short)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((short)~(((short)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(short).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(short).SetBit(Bits.SizeOfInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearLeastSignificantOneBit(short)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantOneBitInt16()
        {
            Assert.AreEqual((short)0b101000, Bits.ClearLeastSignificantOneBit((short)0b101100));
            Assert.AreEqual(default(short), Bits.ClearLeastSignificantOneBit(default(short)));

            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual(default(short), Bits.ClearLeastSignificantOneBit((short)((short)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantOneBit(allBitsSet);
            }

            Assert.AreEqual(default(short), allBitsSet);
        }

        /// <summary>
        /// <see cref="Bits.SetLeastSignificantZeroBit(short)(short)"/>
        /// </summary>
        [Test]
        public void TestSetLeastSignificantZeroBitInt16()
        {
            Assert.AreEqual((short)0b010111, Bits.SetLeastSignificantZeroBit((short)0b010011));
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            Assert.AreEqual(allBitsSet, Bits.SetLeastSignificantZeroBit(allBitsSet));

            var value = default(short);
            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual(allBitsSet, Bits.SetLeastSignificantZeroBit(allBitsSet.ClearBit(i)));
                value = Bits.SetLeastSignificantZeroBit(allBitsSet);
            }

            Assert.AreEqual(allBitsSet, value);
        }

        /// <summary>
        /// <see cref="Bits.SetTrailingZeroBits(short)"/>
        /// </summary>
        [Test]
        public void TestSetTrailingZeroBitsInt16()
        {
            Check(default(short));
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            Check(allBitsSet);
            Check(short.MinValue);
            Check(short.MaxValue);

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Check(Bits.ShiftLeft((short)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomInt16());
            }

            void Check(short value)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = SetTrailingZeroBitsString(valueBits);
                var actual = Bits.SetTrailingZeroBits(value);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"TestSetTrailingZeroBitsInt16({value} /* {valueBits} */)");
            }

            string SetTrailingZeroBitsString(string bits)
            {
                var lastOneBitIndex = bits.LastIndexOf('1');
                return bits.Substring(0, lastOneBitIndex + 1) + new string('1', bits.Length - (lastOneBitIndex + 1));
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantOneBit(short)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantOneBitInt16()
        {
            Assert.AreEqual((short)0b000100, Bits.IsolateLeastSignificantOneBit((short)0b101100));
            Assert.AreEqual(default(short), Bits.IsolateLeastSignificantOneBit(default(short)));

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual(default(short).SetBit(i), Bits.IsolateLeastSignificantOneBit((default(short).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(short).SetBit(i - 1), Bits.IsolateLeastSignificantOneBit(default(short).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantOneBit(short)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantOneBitInt16()
        {
            Assert.AreEqual((short)0b100000, Bits.IsolateMostSignificantOneBit((short)0b101100));
            Assert.AreEqual(default(short), Bits.IsolateMostSignificantOneBit(default(short)));

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Assert.AreEqual(default(short).SetBit(i), Bits.IsolateMostSignificantOneBit((default(short).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(short).SetBit(i), Bits.IsolateMostSignificantOneBit(default(short).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.BitCount(short)"/>
        /// </summary>
        [Test]
        public void TestBitCountInt16()
        {
            Assert.AreEqual(0, Bits.BitCount((short)0));
            Assert.AreEqual(1, Bits.BitCount((short)1));
            Assert.AreEqual(2, Bits.BitCount(default(short).SetBit(Bits.SizeOfInt16InBits / 2).SetBit(Bits.SizeOfInt16InBits - 1)));

            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            Assert.AreEqual(Bits.SizeOfInt16InBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfInt16InBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfInt16InBits / 2).ClearBit(0)));

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt16();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.TrailingZeroBitCount(short)"/>
        /// </summary>
        [Test]
        public void TestTrailingZeroBitCountInt16()
        {
            Assert.AreEqual(Bits.SizeOfInt16InBits, Bits.TrailingZeroBitCount((short)0));
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            Assert.AreEqual(0, Bits.TrailingZeroBitCount(allBitsSet));

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(i + 1, Bits.TrailingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt16();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfInt16InBits : binaryString.Length - binaryString.TrimEnd('0').Length, Bits.TrailingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.LeadingZeroBitCount(short)"/>
        /// </summary>
        [Test]
        public void TestLeadingZeroBitCountInt16()
        {
            Assert.AreEqual(Bits.SizeOfInt16InBits, Bits.LeadingZeroBitCount((short)0));
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            Assert.AreEqual(0, Bits.LeadingZeroBitCount(allBitsSet));

            for (var i = Bits.SizeOfInt16InBits - 1; i >= 0; --i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(Bits.SizeOfInt16InBits - i, Bits.LeadingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt16();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfInt16InBits : Bits.SizeOfInt16InBits - binaryString.Length, Bits.LeadingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.RotateLeft(short, int)"/>
        /// </summary>
        [Test]
        public void TestRotateLeftInt16() => this.TestRotateInt16Helper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.RotateRight(short, int)"/>
        /// </summary>
        [Test]
        public void TestRotateRightInt16() => this.TestRotateInt16Helper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestRotateLeftInt16"/> and <see cref="TestRotateRightInt16"/>
        /// </summary>
        private void TestRotateInt16Helper(bool isLeft)
        {
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfInt16InBits; i < Bits.SizeOfInt16InBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomInt16();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(short value, int positions)
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
                if (actualPositions < 0) { actualPositions += Bits.SizeOfInt16InBits; }
                return isLeft
                    ? bits.Substring(actualPositions) + bits.Substring(0, actualPositions)
                    : bits.Substring(bits.Length - actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.Reverse(short)"/>
        /// </summary>
        [Test]
        public void TestReverseInt16()
        {
            Check(default(short));
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            Check(allBitsSet);
            Check(short.MinValue);
            Check(short.MaxValue);

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Check(Bits.ShiftLeft((short)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomInt16());
            }

            void Check(short value)
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
        /// <see cref="Bits.ReverseBytes(short)"/>
        /// </summary>
        [Test]
        public void TestReverseBytesInt16()
        {
            Check(default(short));
            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            Check(allBitsSet);
            Check(short.MinValue);
            Check(short.MaxValue);

            for (var i = 0; i < Bits.SizeOfInt16InBits; ++i)
            {
                Check(Bits.ShiftLeft((short)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomInt16());
            }

            void Check(short value)
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
        /// <see cref="Bits.ToShortBinaryString(short)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringInt16()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((short)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((short)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((short)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((short)0b01010101)));

            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfInt16InBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(short)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringInt16()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfInt16InBits), Bits.ToLongBinaryString((short)0));
            Assert.AreEqual(new string('0', Bits.SizeOfInt16InBits - 1) + "1", Bits.ToLongBinaryString((short)1));
            Assert.AreEqual(new string('0', Bits.SizeOfInt16InBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((short)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfInt16InBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((short)0b01010101)));

            var allBitsSet = short.MinValue == default(short) ? short.MaxValue : unchecked((short)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfInt16InBits), Bits.ToLongBinaryString(allBitsSet));
        }

        /// <summary>
        /// Helper buffer for <see cref="GetRandomInt16"/>
        /// </summary>
        private readonly byte[] _getRandomBufferInt16 = new byte[sizeof(short)];

        /// <summary>
        /// Helper to generate random <see cref="short"/> values for fuzz testing
        /// </summary>
        private short GetRandomInt16()
        {
            this._random.Value.NextBytes(this._getRandomBufferInt16);
            short value = 0;
            for (var i = 0; i < sizeof(short); ++i)
            {
                value = unchecked((short)((short)(value << 8) & (short)this._getRandomBufferInt16[i]));
            }
            return value;
        }

        
    }
}
