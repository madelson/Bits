//
// AUTO-GENERATED
//
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitwise
{
    public static partial class Bits
    {
        /// <summary>
        /// Size of the <see cref="byte"/> type in bits
        /// </summary>
        [MemberFor(typeof(byte))] // can't be auto-generated because byte members may need this directly
        internal const int SizeOfByteInBits = sizeof(byte) * 8;

        /// <summary>
        /// The native shift operator on <see cref="byte"/> converts to <see cref="int"/> before shifting. This method performs
        /// a shift purely within the confines of the <see cref="byte"/> data type
        /// </summary>
        [MemberFor(typeof(byte))]
        public static byte ShiftLeft(byte value, int positions) => unchecked((byte)(value << (positions & ((sizeof(byte) * 8) - 1))));

        /// <summary>
        /// The native shift operator on <see cref="byte"/> converts to <see cref="int"/> before shifting. This method performs
        /// a shift purely within the confines of the <see cref="byte"/> data type
        /// </summary>
        [MemberFor(typeof(byte))]
        public static byte ShiftRight(byte value, int positions) => unchecked((byte)(value >> (positions & ((sizeof(byte) * 8) - 1))));

        /// <summary>As the native operator, but returns <see cref="byte"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(byte))]
        public static byte And(byte a, byte b) => unchecked((byte)(a & b));

        /// <summary>As the native operator, but returns <see cref="byte"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(byte))]
        public static byte Or(byte a, byte b) => unchecked((byte)(a | b));

        /// <summary>As the native operator, but returns <see cref="byte"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(byte))]
        public static byte Xor(byte a, byte b) => unchecked((byte)(a ^ b));

        /// <summary>As the native operator, but returns <see cref="byte"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(byte))]
        public static byte Not(byte value) => unchecked((byte)~value);

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this byte value, byte flags) => (value & flags) != 0;

        /// <summary>
        /// Determines whether <paramref name="value"/> has all of the bits set that are set in <paramref name="flags"/>
        /// </summary>
        public static bool HasAllFlags(this byte value, byte flags) => (value & flags) == flags;

        /// <summary>
        /// Determines whether the <paramref name="index"/>th bit is set in <paramref name="value"/>
        /// </summary>
        public static bool GetBit(this byte value, int index)
        {
            if ((index & ~(SizeOfByteInBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return value.HasAnyFlag((byte)(((byte)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit set
        /// </summary>
        public static byte SetBit(this byte value, int index)
        {
            if ((index & ~(SizeOfByteInBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (byte)(value | (byte)(((byte)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit cleared
        /// </summary>
        public static byte ClearBit(this byte value, int index)
        {
            if ((index & ~(SizeOfByteInBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (byte)(value & unchecked((byte)~(((byte)1) << index)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static byte FlipBit(this byte value, int index)
        {
            if ((index & ~(SizeOfByteInBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (byte)(value ^ (byte)(((byte)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the least significant bit cleared
        /// </summary>
        public static byte ClearLeastSignificantBit(byte value) => (byte)(value & unchecked(value - 1));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static byte IsolateLeastSignificantSetBit(byte value) => (byte)(value & unchecked((byte)0 - value));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        [MemberFor(typeof(byte))]
        public static byte IsolateMostSignificantSetBit(byte value)
        {
            // the idea here is to steadily set all bits less significant than the most significant bit,
            // and then follow up by clearing them all. See https://stackoverflow.com/questions/28846601/java-integer-highestonebit-in-c-sharp

            value = (byte)(value | (value >> 1));
            value = (byte)(value | (value >> 2));
            value = (byte)(value | (value >> 4));

            // to simplify codegen for smaller integral types, we fork on sizeof().
            // The compiler will remove these branches so that no additional inefficiency
            // is incurred
#pragma warning disable 0162
            if (sizeof(byte) > 1)
            {
                value = (byte)(value | (value >> 8));
                if (sizeof(byte) > 2)
                {
                    value = (byte)(value | (value >> 16));
                    if (sizeof(byte) > 4)
                    {
                        value = (byte)(value | (value >> 32));
                    }
                }
            }
#pragma warning restore 0162

            return (byte)(value - (byte)(value >> 1));
        }

        /// <summary>
        /// Returns the number of set bits in <paramref name="value"/>
        /// </summary>
        [MemberFor(typeof(byte))]
        public static int BitCount(byte value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/6-b14/java/lang/Long.java#Long.bitCount%28sbyte%29
            // also described here: http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetParallel

            unchecked
            {
                value = (byte)(value - (byte)((byte)(value >> 1) & (byte)0x5555555555555555));
                value = (byte)((byte)(value & (byte)0x3333333333333333) + (byte)((byte)(value >> 2) & (byte)0x3333333333333333));
                value = (byte)((byte)(value + (byte)(value >> 4)) & (byte)0x0f0f0f0f0f0f0f0f);

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(byte) > 1)
                {
                    value = (byte)(value + (byte)(value >> 8));
                    if (sizeof(byte) > 2)
                    {
                        value = (byte)(value + (byte)(value >> 16));
                        if (sizeof(byte) > 4)
                        {
                            value = (byte)(value + (byte)(value >> 32));

                            // byte
                            return (int)(value & (byte)0b1111111);
                        }

                        // uint
                        return (int)(value & (byte)0b111111);
                    }

                    // ushort
                    return (int)(value & (byte)0b11111);
                }

                // byte
                return (int)(value & (byte)0b1111);
#pragma warning restore 0162
            }
        }

        /// <summary>
        /// Returns the number of zero bits following the least-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(sbyte)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="sbyte"/> data type
        /// </summary>
        [MemberFor(typeof(byte))]
        public static int TrailingZeroBitCount(byte value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/8u40-b25/java/lang/Integer.java#Integer.numberOfTrailingZeros%28int%29
 
            if (value == (byte)0) { return SizeOfByteInBits; }

            unchecked
            {
                // the algorithm here is really very simple. We start by assuming that we have
                // N-1 trailing zeros (N being special cased above). We then proceed to cut off
                // half of the bits remaining with a left-shift. If that leaves a non-zero remainder,
                // then we know that the lower half has a one and thus our assumption of the answer
                // drops to at most N_lower_half - 1. We only "keep" the shift if this was the case, the
                // result of which is that the bits needed to calculate the rest of the answer are now
                // the top bits

                var count = SizeOfByteInBits - 1;
                byte y;

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(byte) > 4)
                {
                    y = (byte)(value << 32);
                    if (y != 0) { count -= 32; value = y; }
                }

                if (sizeof(byte) > 2)
                {
                    y = (byte)(value << 16);
                    if (y != 0) { count -= 16; value = y; }
                }

                if (sizeof(byte) > 1)
                {
                    y = (byte)(value << 8);
                    if (y != 0) { count -= 8; value = y; }
                }
#pragma warning restore 0162

                y = (byte)(value << 4);
                if (y != 0) { count -= 4; value = y; }

                y = (byte)(value << 2);
                if (y != 0) { count -= 2; value = y; }

                return (int)(count - (int)((byte)(value << 1) >> (SizeOfByteInBits - 1)));
            }
        }

        /// <summary>
        /// Returns the number of zero bits preceding the most-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(sbyte)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="sbyte"/> data type
        /// </summary>
        [MemberFor(typeof(byte))]
        public static int LeadingZeroBitCount(byte value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/8u40-b25/java/lang/Integer.java#Integer.numberOfLeadingZeros%28int%29

            if (value == (byte)0) { return SizeOfByteInBits; }

            unchecked
            {
                var count = 1;

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(byte) > 4)
                {
                    if ((byte)(value >> (SizeOfByteInBits - 32)) == 0) { count += 32; value = (byte)(value << 32); }
                }
                if (sizeof(byte) > 2)
                {
                    if ((byte)(value >> (SizeOfByteInBits - 16)) == 0) { count += 16; value = (byte)(value << 16); }
                }
                if (sizeof(byte) > 1)
                {
                    if ((byte)(value >> (SizeOfByteInBits - 8)) == 0) { count += 8; value = (byte)(value << 8); }
                }
#pragma warning restore 0162
                if ((byte)(value >> (SizeOfByteInBits - 4)) == 0) { count += 4; value = (byte)(value << 4); }
                if ((byte)(value >> (SizeOfByteInBits - 2)) == 0) { count += 2; value = (byte)(value << 2); }
                count -= (int)(value >> (SizeOfByteInBits - 1));
                return count;
            }
        }

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" left by <paramref name="positions"/> bit positions. This is similar
        /// to shifting left, except that bits shifted off the high end reenter on the low end
        /// </summary>
        [MemberFor(typeof(byte))]
        public static byte RotateLeft(byte value, int positions) => unchecked((byte)(ShiftLeft(value, positions) | ShiftRight(value, -positions)));

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" right by <paramref name="positions"/> bit positions. This is similar
        /// to shifting right, except that bits shifted off the low end reenter on the high end
        /// </summary>
        [MemberFor(typeof(byte))]
        public static byte RotateRight(byte value, int positions) => unchecked((byte)(ShiftRight(value, positions) | ShiftLeft(value, -positions)));

        /// <summary>
        /// Returns <paramref name="value"/> with the bits reversed
        /// </summary>
        [MemberFor(typeof(byte))]
        public static byte Reverse(byte value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/8u40-b25/java/lang/Long.java#Long.reverse%28sbyte%29

            // the general approach here is to exchange pairs of bits, then sets of 4 bits, then 8, until all are exchanged. Rather than using the
            // same methodology for all exchanges we can use a shortcut to do the last two exchanges in one pass
            
            unchecked
            {
                // initial exchanges:

                // 0x5555 has all even bits set: exchanges every even bit with the following odd bit
                value = Or(ShiftLeft(And(value, (byte)0x5555555555555555), 1), And(ShiftRight(value, 1), (byte)0x5555555555555555));

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(byte) > 1)
                {
                    // 0x3333 follows the pattern 1100: exchanges every even pair of bits with the following odd pair of bits
                    value = Or(ShiftLeft(And(value, (byte)0x3333333333333333), 2), And(ShiftRight(value, 2), (byte)0x3333333333333333));
                }
                if (sizeof(byte) > 2)
                {
                    // 0xf0f0 follows the pattern 11110000: exchanges every even set of 4 bits with the following odd set of 4 bits
                    value = Or(ShiftLeft(And(value, (byte)0x0f0f0f0f0f0f0f0f), 4), And(ShiftRight(value, 4), (byte)0x0f0f0f0f0f0f0f0f));
                }
                if (sizeof(byte) > 4)
                {
                    // 0x00ff follows the pattern 0000000011111111: exchanges adjacent sets of 8 bits
                    value = Or(ShiftLeft(And(value, (byte)0x00ff00ff00ff00ff), 8), And(ShiftRight(value, 8), (byte)0x00ff00ff00ff00ff));
                }

                // final exchanges:

                // used to exchange the top and bottom fourths
                const int LongShiftValue = 3 * (SizeOfByteInBits / 4);
                // used to exchange the 2nd and 3rd fourths
                const int ShortShiftValue = SizeOfByteInBits / 4;
                // used to isolate the 2nd and 3rd fourths
                const byte SecondFourthMask = (byte)(
                    sizeof(byte) == 1 ? 0b1100
                        : sizeof(byte) == 2 ? 0b11110000
                        : sizeof(byte) == 4 ? 0b1111111100000000
                        : 0xffff0000
                );
#pragma warning restore 0162

                return Or(
                    // bottom fourth shifted to the top
                    ShiftLeft(value, LongShiftValue),
                    Or(
                        // 2nd fourth shifted to the 3rd fourth
                        ShiftLeft(And(value, SecondFourthMask), ShortShiftValue),
                        Or(
                            // 3rd fourth shifted to the 2nd fourth
                            And(ShiftRight(value, ShortShiftValue), SecondFourthMask),
                            // top fourth shifted to the bottom
                            ShiftRight(value, LongShiftValue)
                        )
                    )
                );
            }
        }

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITHOUT leading zeros
        /// </summary>
        [MemberFor(typeof(byte))]
        public static string ToShortBinaryString(byte value) => ToShortBinaryString(unchecked((sbyte)value));

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITH ALL leading zeros
        /// </summary>
        public static string ToLongBinaryString(byte value) => ToShortBinaryString(value).PadLeft(SizeOfByteInBits, '0');
        
        
    }
}
