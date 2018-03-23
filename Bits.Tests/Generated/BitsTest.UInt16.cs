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
        /// <see cref="Bits.ShiftLeft(ushort, int)"/>
        /// </summary>
        [Test]
        public void TestShiftLeftUInt16() => this.TestShiftUInt16Helper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.ShiftRight(ushort, int)"/>
        /// </summary>
        [Test]
        public void TestShiftRightUInt16() => this.TestShiftUInt16Helper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestShiftLeftUInt16"/> and <see cref="TestShiftRightUInt16"/>
        /// </summary>
        private void TestShiftUInt16Helper(bool isLeft)
        {
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfUInt16InBits; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt16();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(ushort value, int positions)
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
                    : new string(CodeGenerator.IsUnsigned(typeof(ushort)) ? '0' : bits[0], actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.And(ushort, ushort)"/>
        /// </summary>
        [Test]
        public void TestAndUInt16()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomUInt16();
                var randomValue2 = this.GetRandomUInt16();
                Assert.AreEqual((ushort)(randomValue1 & randomValue2), Bits.And(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Or(ushort, ushort)"/>
        /// </summary>
        [Test]
        public void TestOrUInt16()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomUInt16();
                var randomValue2 = this.GetRandomUInt16();
                Assert.AreEqual((ushort)(randomValue1 | randomValue2), Bits.Or(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Not(ushort)"/>
        /// </summary>
        [Test]
        public void TestNotUInt16()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt16();
                Assert.AreEqual((ushort)(~randomValue), Bits.Not(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.Xor(ushort, ushort)"/>
        /// </summary>
        [Test]
        public void TestXorUInt16()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomUInt16();
                var randomValue2 = this.GetRandomUInt16();
                Assert.AreEqual((ushort)(randomValue1 ^ randomValue2), Bits.Xor(randomValue1, randomValue2));
            }
        }

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

        /// <summary>
        /// <see cref="Bits.GetBit(ushort, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitUInt16()
        {
            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.IsTrue((((ushort)1) << i).GetBit(i));
                Assert.IsFalse((~(((ushort)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).GetBit(Bits.SizeOfUInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.SetBit(ushort, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitUInt16()
        {
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            var noBitsSet = default(ushort);

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual((ushort)(((ushort)1) << i), default(ushort).SetBit(i));
                Assert.AreEqual(((ushort)25).SetBit(i), ((ushort)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
                noBitsSet = noBitsSet.SetBit(i);
            }

            Assert.AreEqual(allBitsSet, noBitsSet);

            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).SetBit(Bits.SizeOfUInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearBit(ushort, int)"/>
        /// </summary>
        [Test]
        public void TestClearBitUInt16()
        {
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual((ushort)0, default(ushort).ClearBit(i));
                Assert.AreEqual(((ushort)25).ClearBit(i), ((ushort)25).ClearBit(i).ClearBit(i));
                Assert.AreEqual((ushort)~(((ushort)1) << i), allBitsSet.ClearBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).SetBit(Bits.SizeOfUInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(ushort, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitUInt16()
        {
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual((ushort)(((ushort)1) << i), default(ushort).FlipBit(i));
                Assert.AreEqual(((ushort)25), ((ushort)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((ushort)~(((ushort)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(ushort).SetBit(Bits.SizeOfUInt16InBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearLeastSignificantOneBit(ushort)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantOneBitUInt16()
        {
            Assert.AreEqual((ushort)0b101000, Bits.ClearLeastSignificantOneBit((ushort)0b101100));
            Assert.AreEqual(default(ushort), Bits.ClearLeastSignificantOneBit(default(ushort)));

            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual(default(ushort), Bits.ClearLeastSignificantOneBit((ushort)((ushort)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantOneBit(allBitsSet);
            }

            Assert.AreEqual(default(ushort), allBitsSet);
        }

        /// <summary>
        /// <see cref="Bits.SetLeastSignificantZeroBit(ushort)(ushort)"/>
        /// </summary>
        [Test]
        public void TestSetLeastSignificantZeroBitUInt16()
        {
            Assert.AreEqual((ushort)0b010111, Bits.SetLeastSignificantZeroBit((ushort)0b010011));
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Assert.AreEqual(allBitsSet, Bits.SetLeastSignificantZeroBit(allBitsSet));

            var value = default(ushort);
            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual(allBitsSet, Bits.SetLeastSignificantZeroBit(allBitsSet.ClearBit(i)));
                value = Bits.SetLeastSignificantZeroBit(allBitsSet);
            }

            Assert.AreEqual(allBitsSet, value);
        }

        /// <summary>
        /// <see cref="Bits.SetTrailingZeroBits(ushort)"/>
        /// </summary>
        [Test]
        public void TestSetTrailingZeroBitsUInt16()
        {
            Check(default(ushort));
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Check(allBitsSet);
            Check(ushort.MinValue);
            Check(ushort.MaxValue);

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Check(Bits.ShiftLeft((ushort)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomUInt16());
            }

            void Check(ushort value)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = SetTrailingZeroBitsString(valueBits);
                var actual = Bits.SetTrailingZeroBits(value);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"TestSetTrailingZeroBitsUInt16({value} /* {valueBits} */)");
            }

            string SetTrailingZeroBitsString(string bits)
            {
                var lastOneBitIndex = bits.LastIndexOf('1');
                return bits.Substring(0, lastOneBitIndex + 1) + new string('1', bits.Length - (lastOneBitIndex + 1));
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantOneBit(ushort)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantOneBitUInt16()
        {
            Assert.AreEqual((ushort)0b000100, Bits.IsolateLeastSignificantOneBit((ushort)0b101100));
            Assert.AreEqual(default(ushort), Bits.IsolateLeastSignificantOneBit(default(ushort)));

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual(default(ushort).SetBit(i), Bits.IsolateLeastSignificantOneBit((default(ushort).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(ushort).SetBit(i - 1), Bits.IsolateLeastSignificantOneBit(default(ushort).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantOneBit(ushort)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantOneBitUInt16()
        {
            Assert.AreEqual((ushort)0b100000, Bits.IsolateMostSignificantOneBit((ushort)0b101100));
            Assert.AreEqual(default(ushort), Bits.IsolateMostSignificantOneBit(default(ushort)));

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.AreEqual(default(ushort).SetBit(i), Bits.IsolateMostSignificantOneBit((default(ushort).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(ushort).SetBit(i), Bits.IsolateMostSignificantOneBit(default(ushort).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.BitCount(ushort)"/>
        /// </summary>
        [Test]
        public void TestBitCountUInt16()
        {
            Assert.AreEqual(0, Bits.BitCount((ushort)0));
            Assert.AreEqual(1, Bits.BitCount((ushort)1));
            Assert.AreEqual(2, Bits.BitCount(default(ushort).SetBit(Bits.SizeOfUInt16InBits / 2).SetBit(Bits.SizeOfUInt16InBits - 1)));

            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Assert.AreEqual(Bits.SizeOfUInt16InBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfUInt16InBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfUInt16InBits / 2).ClearBit(0)));

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt16();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.TrailingZeroBitCount(ushort)"/>
        /// </summary>
        [Test]
        public void TestTrailingZeroBitCountUInt16()
        {
            Assert.AreEqual(Bits.SizeOfUInt16InBits, Bits.TrailingZeroBitCount((ushort)0));
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Assert.AreEqual(0, Bits.TrailingZeroBitCount(allBitsSet));

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(i + 1, Bits.TrailingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt16();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfUInt16InBits : binaryString.Length - binaryString.TrimEnd('0').Length, Bits.TrailingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.LeadingZeroBitCount(ushort)"/>
        /// </summary>
        [Test]
        public void TestLeadingZeroBitCountUInt16()
        {
            Assert.AreEqual(Bits.SizeOfUInt16InBits, Bits.LeadingZeroBitCount((ushort)0));
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Assert.AreEqual(0, Bits.LeadingZeroBitCount(allBitsSet));

            for (var i = Bits.SizeOfUInt16InBits - 1; i >= 0; --i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(Bits.SizeOfUInt16InBits - i, Bits.LeadingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt16();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfUInt16InBits : Bits.SizeOfUInt16InBits - binaryString.Length, Bits.LeadingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.HasSingleOneBit(ushort)"/>
        /// </summary>
        [Test]
        public void TestHasSingleOneBitUInt16()
        {
            Assert.IsTrue(Bits.HasSingleOneBit((ushort)1));
            Assert.IsFalse(Bits.HasSingleOneBit((ushort)0));
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Assert.IsFalse(Bits.HasSingleOneBit(allBitsSet));

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Assert.IsTrue(Bits.HasSingleOneBit(Bits.ShiftLeft((ushort)1, i)));
                Assert.IsFalse(Bits.HasSingleOneBit(Bits.Not(Bits.ShiftLeft((ushort)1, i))));
                if (i < Bits.SizeOfUInt16InBits - 1)
                {
                    Assert.IsFalse(Bits.HasSingleOneBit(Bits.ShiftLeft((ushort)0b11, i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.RotateLeft(ushort, int)"/>
        /// </summary>
        [Test]
        public void TestRotateLeftUInt16() => this.TestRotateUInt16Helper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.RotateRight(ushort, int)"/>
        /// </summary>
        [Test]
        public void TestRotateRightUInt16() => this.TestRotateUInt16Helper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestRotateLeftUInt16"/> and <see cref="TestRotateRightUInt16"/>
        /// </summary>
        private void TestRotateUInt16Helper(bool isLeft)
        {
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfUInt16InBits; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomUInt16();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(ushort value, int positions)
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
                if (actualPositions < 0) { actualPositions += Bits.SizeOfUInt16InBits; }
                return isLeft
                    ? bits.Substring(actualPositions) + bits.Substring(0, actualPositions)
                    : bits.Substring(bits.Length - actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.Reverse(ushort)"/>
        /// </summary>
        [Test]
        public void TestReverseUInt16()
        {
            Check(default(ushort));
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Check(allBitsSet);
            Check(ushort.MinValue);
            Check(ushort.MaxValue);

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Check(Bits.ShiftLeft((ushort)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomUInt16());
            }

            void Check(ushort value)
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
        /// <see cref="Bits.ReverseBytes(ushort)"/>
        /// </summary>
        [Test]
        public void TestReverseBytesUInt16()
        {
            Check(default(ushort));
            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Check(allBitsSet);
            Check(ushort.MinValue);
            Check(ushort.MaxValue);

            for (var i = 0; i < Bits.SizeOfUInt16InBits; ++i)
            {
                Check(Bits.ShiftLeft((ushort)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomUInt16());
            }

            void Check(ushort value)
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
        /// <see cref="Bits.ToShortBinaryString(ushort)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringUInt16()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((ushort)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((ushort)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((ushort)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((ushort)0b01010101)));

            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfUInt16InBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(ushort)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringUInt16()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfUInt16InBits), Bits.ToLongBinaryString((ushort)0));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt16InBits - 1) + "1", Bits.ToLongBinaryString((ushort)1));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt16InBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((ushort)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfUInt16InBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((ushort)0b01010101)));

            var allBitsSet = ushort.MinValue == default(ushort) ? ushort.MaxValue : unchecked((ushort)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfUInt16InBits), Bits.ToLongBinaryString(allBitsSet));
        }

        /// <summary>
        /// Helper buffer for <see cref="GetRandomUInt16"/>
        /// </summary>
        private readonly byte[] _getRandomBufferUInt16 = new byte[sizeof(ushort)];

        /// <summary>
        /// Helper to generate random <see cref="ushort"/> values for fuzz testing
        /// </summary>
        private ushort GetRandomUInt16()
        {
            this._random.Value.NextBytes(this._getRandomBufferUInt16);
            ushort value = 0;
            for (var i = 0; i < sizeof(ushort); ++i)
            {
                value = unchecked((ushort)((ushort)(value << 8) & (ushort)this._getRandomBufferUInt16[i]));
            }
            return value;
        }

        
    }
}
