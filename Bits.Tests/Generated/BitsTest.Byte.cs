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
        /// <see cref="Bits.ShiftLeft(byte, int)"/>
        /// </summary>
        [Test]
        public void TestShiftLeftByte() => this.TestShiftByteHelper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.ShiftRight(byte, int)"/>
        /// </summary>
        [Test]
        public void TestShiftRightByte() => this.TestShiftByteHelper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestShiftLeftByte"/> and <see cref="TestShiftRightByte"/>
        /// </summary>
        private void TestShiftByteHelper(bool isLeft)
        {
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfByteInBits; i < Bits.SizeOfByteInBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomByte();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(byte value, int positions)
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
                    : new string(CodeGenerator.IsUnsigned(typeof(byte)) ? '0' : bits[0], actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.And(byte, byte)"/>
        /// </summary>
        [Test]
        public void TestAndByte()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomByte();
                var randomValue2 = this.GetRandomByte();
                Assert.AreEqual((byte)(randomValue1 & randomValue2), Bits.And(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Or(byte, byte)"/>
        /// </summary>
        [Test]
        public void TestOrByte()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomByte();
                var randomValue2 = this.GetRandomByte();
                Assert.AreEqual((byte)(randomValue1 | randomValue2), Bits.Or(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Not(byte)"/>
        /// </summary>
        [Test]
        public void TestNotByte()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomByte();
                Assert.AreEqual((byte)(~randomValue), Bits.Not(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.Xor(byte, byte)"/>
        /// </summary>
        [Test]
        public void TestXorByte()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomByte();
                var randomValue2 = this.GetRandomByte();
                Assert.AreEqual((byte)(randomValue1 ^ randomValue2), Bits.Xor(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.HasAnyFlag(byte, byte)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagByte()
        {
            Assert.IsTrue(((byte)1).HasAnyFlag(1));
            Assert.IsTrue(((byte)3).HasAnyFlag(2));
            Assert.IsFalse(((byte)18).HasAnyFlag(9));
            Assert.IsFalse(byte.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((byte)0).HasAnyFlag(0));
        }

        /// <summary>
        /// <see cref="Bits.HasAllFlags(byte, byte)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsByte()
        {
            Assert.IsTrue(((byte)1).HasAllFlags(1));
            Assert.IsTrue(((byte)3).HasAllFlags(2));
            Assert.IsFalse(((byte)18).HasAllFlags(9));
            Assert.IsTrue(byte.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((byte)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(byte, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitByte()
        {
            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.IsTrue((((byte)1) << i).GetBit(i));
                Assert.IsFalse((~(((byte)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(byte).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(byte).GetBit(Bits.SizeOfByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.SetBit(byte, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitByte()
        {
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            var noBitsSet = default(byte);

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual((byte)(((byte)1) << i), default(byte).SetBit(i));
                Assert.AreEqual(((byte)25).SetBit(i), ((byte)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
                noBitsSet = noBitsSet.SetBit(i);
            }

            Assert.AreEqual(allBitsSet, noBitsSet);

            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(Bits.SizeOfByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearBit(byte, int)"/>
        /// </summary>
        [Test]
        public void TestClearBitByte()
        {
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual((byte)0, default(byte).ClearBit(i));
                Assert.AreEqual(((byte)25).ClearBit(i), ((byte)25).ClearBit(i).ClearBit(i));
                Assert.AreEqual((byte)~(((byte)1) << i), allBitsSet.ClearBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(Bits.SizeOfByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(byte, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitByte()
        {
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual((byte)(((byte)1) << i), default(byte).FlipBit(i));
                Assert.AreEqual(((byte)25), ((byte)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((byte)~(((byte)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(byte).SetBit(Bits.SizeOfByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearLeastSignificantOneBit(byte)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantOneBitByte()
        {
            Assert.AreEqual((byte)0b101000, Bits.ClearLeastSignificantOneBit((byte)0b101100));
            Assert.AreEqual(default(byte), Bits.ClearLeastSignificantOneBit(default(byte)));

            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual(default(byte), Bits.ClearLeastSignificantOneBit((byte)((byte)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantOneBit(allBitsSet);
            }

            Assert.AreEqual(default(byte), allBitsSet);
        }

        /// <summary>
        /// <see cref="Bits.SetLeastSignificantZeroBit(byte)(byte)"/>
        /// </summary>
        [Test]
        public void TestSetLeastSignificantZeroBitByte()
        {
            Assert.AreEqual((byte)0b010111, Bits.SetLeastSignificantZeroBit((byte)0b010011));
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Assert.AreEqual(allBitsSet, Bits.SetLeastSignificantZeroBit(allBitsSet));

            var value = default(byte);
            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual(allBitsSet, Bits.SetLeastSignificantZeroBit(allBitsSet.ClearBit(i)));
                value = Bits.SetLeastSignificantZeroBit(allBitsSet);
            }

            Assert.AreEqual(allBitsSet, value);
        }

        /// <summary>
        /// <see cref="Bits.SetTrailingZeroBits(byte)"/>
        /// </summary>
        [Test]
        public void TestSetTrailingZeroBitsByte()
        {
            Check(default(byte));
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Check(allBitsSet);
            Check(byte.MinValue);
            Check(byte.MaxValue);

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Check(Bits.ShiftLeft((byte)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomByte());
            }

            void Check(byte value)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = SetTrailingZeroBitsString(valueBits);
                var actual = Bits.SetTrailingZeroBits(value);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"TestSetTrailingZeroBitsByte({value} /* {valueBits} */)");
            }

            string SetTrailingZeroBitsString(string bits)
            {
                var lastOneBitIndex = bits.LastIndexOf('1');
                return bits.Substring(0, lastOneBitIndex + 1) + new string('1', bits.Length - (lastOneBitIndex + 1));
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantOneBit(byte)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantOneBitByte()
        {
            Assert.AreEqual((byte)0b000100, Bits.IsolateLeastSignificantOneBit((byte)0b101100));
            Assert.AreEqual(default(byte), Bits.IsolateLeastSignificantOneBit(default(byte)));

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual(default(byte).SetBit(i), Bits.IsolateLeastSignificantOneBit((default(byte).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(byte).SetBit(i - 1), Bits.IsolateLeastSignificantOneBit(default(byte).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantOneBit(byte)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantOneBitByte()
        {
            Assert.AreEqual((byte)0b100000, Bits.IsolateMostSignificantOneBit((byte)0b101100));
            Assert.AreEqual(default(byte), Bits.IsolateMostSignificantOneBit(default(byte)));

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Assert.AreEqual(default(byte).SetBit(i), Bits.IsolateMostSignificantOneBit((default(byte).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(byte).SetBit(i), Bits.IsolateMostSignificantOneBit(default(byte).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.BitCount(byte)"/>
        /// </summary>
        [Test]
        public void TestBitCountByte()
        {
            Assert.AreEqual(0, Bits.BitCount((byte)0));
            Assert.AreEqual(1, Bits.BitCount((byte)1));
            Assert.AreEqual(2, Bits.BitCount(default(byte).SetBit(Bits.SizeOfByteInBits / 2).SetBit(Bits.SizeOfByteInBits - 1)));

            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Assert.AreEqual(Bits.SizeOfByteInBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfByteInBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfByteInBits / 2).ClearBit(0)));

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomByte();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.TrailingZeroBitCount(byte)"/>
        /// </summary>
        [Test]
        public void TestTrailingZeroBitCountByte()
        {
            Assert.AreEqual(Bits.SizeOfByteInBits, Bits.TrailingZeroBitCount((byte)0));
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Assert.AreEqual(0, Bits.TrailingZeroBitCount(allBitsSet));

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(i + 1, Bits.TrailingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomByte();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfByteInBits : binaryString.Length - binaryString.TrimEnd('0').Length, Bits.TrailingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.LeadingZeroBitCount(byte)"/>
        /// </summary>
        [Test]
        public void TestLeadingZeroBitCountByte()
        {
            Assert.AreEqual(Bits.SizeOfByteInBits, Bits.LeadingZeroBitCount((byte)0));
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Assert.AreEqual(0, Bits.LeadingZeroBitCount(allBitsSet));

            for (var i = Bits.SizeOfByteInBits - 1; i >= 0; --i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(Bits.SizeOfByteInBits - i, Bits.LeadingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomByte();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfByteInBits : Bits.SizeOfByteInBits - binaryString.Length, Bits.LeadingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.RotateLeft(byte, int)"/>
        /// </summary>
        [Test]
        public void TestRotateLeftByte() => this.TestRotateByteHelper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.RotateRight(byte, int)"/>
        /// </summary>
        [Test]
        public void TestRotateRightByte() => this.TestRotateByteHelper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestRotateLeftByte"/> and <see cref="TestRotateRightByte"/>
        /// </summary>
        private void TestRotateByteHelper(bool isLeft)
        {
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfByteInBits; i < Bits.SizeOfByteInBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomByte();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(byte value, int positions)
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
                if (actualPositions < 0) { actualPositions += Bits.SizeOfByteInBits; }
                return isLeft
                    ? bits.Substring(actualPositions) + bits.Substring(0, actualPositions)
                    : bits.Substring(bits.Length - actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.Reverse(byte)"/>
        /// </summary>
        [Test]
        public void TestReverseByte()
        {
            Check(default(byte));
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Check(allBitsSet);
            Check(byte.MinValue);
            Check(byte.MaxValue);

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Check(Bits.ShiftLeft((byte)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomByte());
            }

            void Check(byte value)
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
        /// <see cref="Bits.ReverseBytes(byte)"/>
        /// </summary>
        [Test]
        public void TestReverseBytesByte()
        {
            Check(default(byte));
            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Check(allBitsSet);
            Check(byte.MinValue);
            Check(byte.MaxValue);

            for (var i = 0; i < Bits.SizeOfByteInBits; ++i)
            {
                Check(Bits.ShiftLeft((byte)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomByte());
            }

            void Check(byte value)
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
        /// <see cref="Bits.ToShortBinaryString(byte)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringByte()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((byte)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((byte)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((byte)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((byte)0b01010101)));

            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfByteInBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(byte)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringByte()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfByteInBits), Bits.ToLongBinaryString((byte)0));
            Assert.AreEqual(new string('0', Bits.SizeOfByteInBits - 1) + "1", Bits.ToLongBinaryString((byte)1));
            Assert.AreEqual(new string('0', Bits.SizeOfByteInBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((byte)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfByteInBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((byte)0b01010101)));

            var allBitsSet = byte.MinValue == default(byte) ? byte.MaxValue : unchecked((byte)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfByteInBits), Bits.ToLongBinaryString(allBitsSet));
        }

        /// <summary>
        /// Helper buffer for <see cref="GetRandomByte"/>
        /// </summary>
        private readonly byte[] _getRandomBufferByte = new byte[sizeof(byte)];

        /// <summary>
        /// Helper to generate random <see cref="byte"/> values for fuzz testing
        /// </summary>
        private byte GetRandomByte()
        {
            this._random.Value.NextBytes(this._getRandomBufferByte);
            byte value = 0;
            for (var i = 0; i < sizeof(byte); ++i)
            {
                value = unchecked((byte)((byte)(value << 8) & (byte)this._getRandomBufferByte[i]));
            }
            return value;
        }

        
    }
}
