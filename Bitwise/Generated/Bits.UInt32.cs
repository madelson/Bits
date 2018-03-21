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
        /// Size of the <see cref="uint"/> type in bits
        /// </summary>
        [MemberFor(typeof(uint))] // can't be auto-generated because uint members may need this directly
        internal const int SizeOfUInt32InBits = sizeof(uint) * 8;

        /// <summary>
        /// The native shift operator on <see cref="uint"/> converts to <see cref="int"/> before shifting. This method performs
        /// a shift purely within the confines of the <see cref="uint"/> data type
        /// </summary>
        [MemberFor(typeof(uint))]
        public static uint ShiftLeft(uint value, int positions) => unchecked((uint)(value << (positions & ((sizeof(uint) * 8) - 1))));

        /// <summary>
        /// The native shift operator on <see cref="uint"/> converts to <see cref="int"/> before shifting. This method performs
        /// a shift purely within the confines of the <see cref="uint"/> data type
        /// </summary>
        [MemberFor(typeof(uint))]
        public static uint ShiftRight(uint value, int positions) => unchecked((uint)(value >> (positions & ((sizeof(uint) * 8) - 1))));

        /// <summary>As the native operator, but returns <see cref="uint"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(uint))]
        public static uint And(uint a, uint b) => unchecked((uint)(a & b));

        /// <summary>As the native operator, but returns <see cref="uint"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(uint))]
        public static uint Or(uint a, uint b) => unchecked((uint)(a | b));

        /// <summary>As the native operator, but returns <see cref="uint"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(uint))]
        public static uint Xor(uint a, uint b) => unchecked((uint)(a ^ b));

        /// <summary>As the native operator, but returns <see cref="uint"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(uint))]
        public static uint Not(uint value) => unchecked((uint)~value);

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this uint value, uint flags) => (value & flags) != 0;

        /// <summary>
        /// Determines whether <paramref name="value"/> has all of the bits set that are set in <paramref name="flags"/>
        /// </summary>
        public static bool HasAllFlags(this uint value, uint flags) => (value & flags) == flags;

        /// <summary>
        /// Determines whether the <paramref name="index"/>th bit is set in <paramref name="value"/>
        /// </summary>
        public static bool GetBit(this uint value, int index)
        {
            if ((index & ~(SizeOfUInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return value.HasAnyFlag((uint)(((uint)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit set
        /// </summary>
        public static uint SetBit(this uint value, int index)
        {
            if ((index & ~(SizeOfUInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (uint)(value | (uint)(((uint)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit cleared
        /// </summary>
        public static uint ClearBit(this uint value, int index)
        {
            if ((index & ~(SizeOfUInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (uint)(value & unchecked((uint)~(((uint)1) << index)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static uint FlipBit(this uint value, int index)
        {
            if ((index & ~(SizeOfUInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (uint)(value ^ (uint)(((uint)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the least significant bit cleared
        /// </summary>
        public static uint ClearLeastSignificantBit(uint value) => (uint)(value & unchecked(value - 1));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static uint IsolateLeastSignificantSetBit(uint value) => (uint)(value & unchecked((uint)0 - value));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        [MemberFor(typeof(uint))]
        public static uint IsolateMostSignificantSetBit(uint value)
        {
            // the idea here is to steadily set all bits less significant than the most significant bit,
            // and then follow up by clearing them all. See https://stackoverflow.com/questions/28846601/java-integer-highestonebit-in-c-sharp

            value = (uint)(value | (value >> 1));
            value = (uint)(value | (value >> 2));
            value = (uint)(value | (value >> 4));

            // to simplify codegen for smaller integral types, we fork on sizeof().
            // The compiler will remove these branches so that no additional inefficiency
            // is incurred
#pragma warning disable 0162
            if (sizeof(uint) > 1)
            {
                value = (uint)(value | (value >> 8));
                if (sizeof(uint) > 2)
                {
                    value = (uint)(value | (value >> 16));
                    if (sizeof(uint) > 4)
                    {
                        value = (uint)(value | (value >> 32));
                    }
                }
            }
#pragma warning restore 0162

            return (uint)(value - (uint)(value >> 1));
        }

        /// <summary>
        /// Returns the number of set bits in <paramref name="value"/>
        /// </summary>
        [MemberFor(typeof(uint))]
        public static int BitCount(uint value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/6-b14/java/lang/Long.java#Long.bitCount%28int%29
            // also described here: http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetParallel

            unchecked
            {
                value = (uint)(value - (uint)((uint)(value >> 1) & (uint)0x5555555555555555));
                value = (uint)((uint)(value & (uint)0x3333333333333333) + (uint)((uint)(value >> 2) & (uint)0x3333333333333333));
                value = (uint)((uint)(value + (uint)(value >> 4)) & (uint)0x0f0f0f0f0f0f0f0f);

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(uint) > 1)
                {
                    value = (uint)(value + (uint)(value >> 8));
                    if (sizeof(uint) > 2)
                    {
                        value = (uint)(value + (uint)(value >> 16));
                        if (sizeof(uint) > 4)
                        {
                            value = (uint)(value + (uint)(value >> 32));

                            // uint
                            return (int)(value & (uint)0b1111111);
                        }

                        // uint
                        return (int)(value & (uint)0b111111);
                    }

                    // ushort
                    return (int)(value & (uint)0b11111);
                }

                // byte
                return (int)(value & (uint)0b1111);
#pragma warning restore 0162
            }
        }

        /// <summary>
        /// Returns the number of zero bits following the least-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(int)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="int"/> data type
        /// </summary>
        [MemberFor(typeof(uint))]
        public static int TrailingZeroBitCount(uint value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/8u40-b25/java/lang/Integer.java#Integer.numberOfTrailingZeros%28int%29
 
            if (value == (uint)0) { return SizeOfUInt32InBits; }

            unchecked
            {
                // the algorithm here is really very simple. We start by assuming that we have
                // N-1 trailing zeros (N being special cased above). We then proceed to cut off
                // half of the bits remaining with a left-shift. If that leaves a non-zero remainder,
                // then we know that the lower half has a one and thus our assumption of the answer
                // drops to at most N_lower_half - 1. We only "keep" the shift if this was the case, the
                // result of which is that the bits needed to calculate the rest of the answer are now
                // the top bits

                var count = SizeOfUInt32InBits - 1;
                uint y;

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(uint) > 4)
                {
                    y = (uint)(value << 32);
                    if (y != 0) { count -= 32; value = y; }
                }

                if (sizeof(uint) > 2)
                {
                    y = (uint)(value << 16);
                    if (y != 0) { count -= 16; value = y; }
                }

                if (sizeof(uint) > 1)
                {
                    y = (uint)(value << 8);
                    if (y != 0) { count -= 8; value = y; }
                }
#pragma warning restore 0162

                y = (uint)(value << 4);
                if (y != 0) { count -= 4; value = y; }

                y = (uint)(value << 2);
                if (y != 0) { count -= 2; value = y; }

                return (int)(count - (int)((uint)(value << 1) >> (SizeOfUInt32InBits - 1)));
            }
        }

        /// <summary>
        /// Returns the number of zero bits preceding the most-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(int)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="int"/> data type
        /// </summary>
        [MemberFor(typeof(uint))]
        public static int LeadingZeroBitCount(uint value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/8u40-b25/java/lang/Integer.java#Integer.numberOfLeadingZeros%28int%29

            if (value == (uint)0) { return SizeOfUInt32InBits; }

            unchecked
            {
                var count = 1;

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(uint) > 4)
                {
                    if ((uint)(value >> (SizeOfUInt32InBits - 32)) == 0) { count += 32; value = (uint)(value << 32); }
                }
                if (sizeof(uint) > 2)
                {
                    if ((uint)(value >> (SizeOfUInt32InBits - 16)) == 0) { count += 16; value = (uint)(value << 16); }
                }
                if (sizeof(uint) > 1)
                {
                    if ((uint)(value >> (SizeOfUInt32InBits - 8)) == 0) { count += 8; value = (uint)(value << 8); }
                }
#pragma warning restore 0162
                if ((uint)(value >> (SizeOfUInt32InBits - 4)) == 0) { count += 4; value = (uint)(value << 4); }
                if ((uint)(value >> (SizeOfUInt32InBits - 2)) == 0) { count += 2; value = (uint)(value << 2); }
                count -= (int)(value >> (SizeOfUInt32InBits - 1));
                return count;
            }
        }

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" left by <paramref name="positions"/> bit positions. This is similar
        /// to shifting left, except that bits shifted off the high end reenter on the low end
        /// </summary>
        [MemberFor(typeof(uint))]
        public static uint RotateLeft(uint value, int positions) => unchecked((uint)(ShiftLeft(value, positions) | ShiftRight(value, -positions)));

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" right by <paramref name="positions"/> bit positions. This is similar
        /// to shifting right, except that bits shifted off the low end reenter on the high end
        /// </summary>
        [MemberFor(typeof(uint))]
        public static uint RotateRight(uint value, int positions) => unchecked((uint)(ShiftRight(value, positions) | ShiftLeft(value, -positions)));

        /// <summary>
        /// Returns <paramref name="value"/> with the bits reversed
        /// </summary>
        [MemberFor(typeof(uint))]
        public static uint Reverse(uint value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/8u40-b25/java/lang/Long.java#Long.reverse%28int%29

            // the general approach here is to exchange pairs of bits, then sets of 4 bits, then 8, until all are exchanged. Rather than using the
            // same methodology for all exchanges we can use a shortcut to do the last two exchanges in one pass
            
            unchecked
            {
                // initial exchanges:

                // 0x5555 has all even bits set: exchanges every even bit with the following odd bit
                value = Or(ShiftLeft(And(value, (uint)0x5555555555555555), 1), And(ShiftRight(value, 1), (uint)0x5555555555555555));

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(uint) > 1)
                {
                    // 0x3333 follows the pattern 1100: exchanges every even pair of bits with the following odd pair of bits
                    value = Or(ShiftLeft(And(value, (uint)0x3333333333333333), 2), And(ShiftRight(value, 2), (uint)0x3333333333333333));
                }
                if (sizeof(uint) > 2)
                {
                    // 0xf0f0 follows the pattern 11110000: exchanges every even set of 4 bits with the following odd set of 4 bits
                    value = Or(ShiftLeft(And(value, (uint)0x0f0f0f0f0f0f0f0f), 4), And(ShiftRight(value, 4), (uint)0x0f0f0f0f0f0f0f0f));
                }
                if (sizeof(uint) > 4)
                {
                    // 0x00ff follows the pattern 0000000011111111: exchanges adjacent sets of 8 bits
                    value = Or(ShiftLeft(And(value, (uint)0x00ff00ff00ff00ff), 8), And(ShiftRight(value, 8), (uint)0x00ff00ff00ff00ff));
                }

                // final exchanges:

                // used to exchange the top and bottom fourths
                const int LongShiftValue = 3 * (SizeOfUInt32InBits / 4);
                // used to exchange the 2nd and 3rd fourths
                const int ShortShiftValue = SizeOfUInt32InBits / 4;
                // used to isolate the 2nd and 3rd fourths
                const uint SecondFourthMask = (uint)(
                    sizeof(uint) == 1 ? 0b1100
                        : sizeof(uint) == 2 ? 0b11110000
                        : sizeof(uint) == 4 ? 0b1111111100000000
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
        [MemberFor(typeof(uint))]
        public static string ToShortBinaryString(uint value) => ToShortBinaryString(unchecked((int)value));

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITH ALL leading zeros
        /// </summary>
        public static string ToLongBinaryString(uint value) => ToShortBinaryString(value).PadLeft(SizeOfUInt32InBits, '0');
        
        
    }
}
