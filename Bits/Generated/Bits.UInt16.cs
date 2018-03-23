//
// AUTO-GENERATED
//
using System;
using System.Collections.Generic;
using System.Text;

namespace Medallion
{
    public static partial class Bits
    {
        /// <summary>
        /// Size of the <see cref="ushort"/> type in bits
        /// </summary>
        [MemberFor(typeof(ushort))] // can't be auto-generated because ushort members may need this directly
        internal const int SizeOfUInt16InBits = sizeof(ushort) * 8;

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this ushort value, ushort flags) => (value & flags) != 0;

        /// <summary>
        /// Determines whether <paramref name="value"/> has all of the bits set that are set in <paramref name="flags"/>
        /// </summary>
        public static bool HasAllFlags(this ushort value, ushort flags) => (value & flags) == flags;

        /// <summary>
        /// Determines whether the <paramref name="index"/>th bit is set in <paramref name="value"/>
        /// </summary>
        public static bool GetBit(this ushort value, int index)
        {
            if ((index & ~(SizeOfUInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return value.HasAnyFlag((ushort)(((ushort)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit set
        /// </summary>
        public static ushort SetBit(this ushort value, int index)
        {
            if ((index & ~(SizeOfUInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ushort)(value | (ushort)(((ushort)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit cleared
        /// </summary>
        public static ushort ClearBit(this ushort value, int index)
        {
            if ((index & ~(SizeOfUInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ushort)(value & unchecked((ushort)~(((ushort)1) << index)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static ushort FlipBit(this ushort value, int index)
        {
            if ((index & ~(SizeOfUInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ushort)(value ^ (ushort)(((ushort)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the least significant set bit cleared
        /// </summary>
        public static ushort ClearLeastSignificantOneBit(ushort value) => (ushort)(value & unchecked(value - 1));
        
        /// <summary>
        /// Return s<paramref name="value"/> with the least significant zero bit set
        /// </summary>
        public static ushort SetLeastSignificantZeroBit(ushort value) => unchecked((ushort)(value | (ushort)(value + 1)));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits less significant than the least significant set bit will be set.
        /// If <paramref name="value"/> is zero then all bits are trailing zero bits so the returned value will have all bits set
        /// </summary>
        public static ushort SetTrailingZeroBits(ushort value) => unchecked((ushort)(value | (ushort)(value - 1)));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static ushort IsolateLeastSignificantOneBit(ushort value) => (ushort)(value & unchecked((ushort)0 - value));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        [MemberFor(typeof(ushort))]
        public static ushort IsolateMostSignificantOneBit(ushort value)
        {
            // the idea here is to steadily set all bits less significant than the most significant bit,
            // and then follow up by clearing them all. See https://stackoverflow.com/questions/28846601/java-integer-highestonebit-in-c-sharp

            value = (ushort)(value | (value >> 1));
            value = (ushort)(value | (value >> 2));
            value = (ushort)(value | (value >> 4));

            // to simplify codegen for smaller integral types, we fork on sizeof().
            // The compiler will remove these branches so that no additional inefficiency
            // is incurred
#pragma warning disable 0162
            if (sizeof(ushort) > 1)
            {
                value = (ushort)(value | (value >> 8));
                if (sizeof(ushort) > 2)
                {
                    value = (ushort)(value | (value >> 16));
                    if (sizeof(ushort) > 4)
                    {
                        value = (ushort)(value | (value >> 32));
                    }
                }
            }
#pragma warning restore 0162

            return unchecked((ushort)(value - (ushort)(value >> 1)));
        }

        /// <summary>
        /// Returns the number of set bits in <paramref name="value"/>
        /// </summary>
        [MemberFor(typeof(ushort))]
        public static int BitCount(ushort value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/6-b14/java/lang/Long.java#Long.bitCount%28short%29
            // also described here: http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetParallel

            unchecked
            {
                value = (ushort)(value - (ushort)((ushort)(value >> 1) & (ushort)0x5555555555555555));
                value = (ushort)((ushort)(value & (ushort)0x3333333333333333) + (ushort)((ushort)(value >> 2) & (ushort)0x3333333333333333));
                value = (ushort)((ushort)(value + (ushort)(value >> 4)) & (ushort)0x0f0f0f0f0f0f0f0f);

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(ushort) > 1)
                {
                    value = (ushort)(value + (ushort)(value >> 8));
                    if (sizeof(ushort) > 2)
                    {
                        value = (ushort)(value + (ushort)(value >> 16));
                        if (sizeof(ushort) > 4)
                        {
                            value = (ushort)(value + (ushort)(value >> 32));

                            // ushort
                            return (int)(value & (ushort)0b1111111);
                        }

                        // uint
                        return (int)(value & (ushort)0b111111);
                    }

                    // ushort
                    return (int)(value & (ushort)0b11111);
                }

                // byte
                return (int)(value & (ushort)0b1111);
#pragma warning restore 0162
            }
        }

        /// <summary>
        /// Returns the number of zero bits following the least-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(short)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="short"/> data type
        /// </summary>
        [MemberFor(typeof(ushort))]
        public static int TrailingZeroBitCount(ushort value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/8u40-b25/java/lang/Integer.java#Integer.numberOfTrailingZeros%28int%29
 
            if (value == (ushort)0) { return SizeOfUInt16InBits; }

            unchecked
            {
                // the algorithm here is really very simple. We start by assuming that we have
                // N-1 trailing zeros (N being special cased above). We then proceed to cut off
                // half of the bits remaining with a left-shift. If that leaves a non-zero remainder,
                // then we know that the lower half has a one and thus our assumption of the answer
                // drops to at most N_lower_half - 1. We only "keep" the shift if this was the case, the
                // result of which is that the bits needed to calculate the rest of the answer are now
                // the top bits

                var count = SizeOfUInt16InBits - 1;
                ushort y;

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(ushort) > 4)
                {
                    y = (ushort)(value << 32);
                    if (y != 0) { count -= 32; value = y; }
                }

                if (sizeof(ushort) > 2)
                {
                    y = (ushort)(value << 16);
                    if (y != 0) { count -= 16; value = y; }
                }

                if (sizeof(ushort) > 1)
                {
                    y = (ushort)(value << 8);
                    if (y != 0) { count -= 8; value = y; }
                }
#pragma warning restore 0162

                y = (ushort)(value << 4);
                if (y != 0) { count -= 4; value = y; }

                y = (ushort)(value << 2);
                if (y != 0) { count -= 2; value = y; }

                return (int)(count - (int)((ushort)(value << 1) >> (SizeOfUInt16InBits - 1)));
            }
        }

        /// <summary>
        /// Returns the number of zero bits preceding the most-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(short)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="short"/> data type
        /// </summary>
        [MemberFor(typeof(ushort))]
        public static int LeadingZeroBitCount(ushort value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/8u40-b25/java/lang/Integer.java#Integer.numberOfLeadingZeros%28int%29

            if (value == (ushort)0) { return SizeOfUInt16InBits; }

            unchecked
            {
                var count = 1;

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(ushort) > 4)
                {
                    if ((ushort)(value >> (SizeOfUInt16InBits - 32)) == 0) { count += 32; value = (ushort)(value << 32); }
                }
                if (sizeof(ushort) > 2)
                {
                    if ((ushort)(value >> (SizeOfUInt16InBits - 16)) == 0) { count += 16; value = (ushort)(value << 16); }
                }
                if (sizeof(ushort) > 1)
                {
                    if ((ushort)(value >> (SizeOfUInt16InBits - 8)) == 0) { count += 8; value = (ushort)(value << 8); }
                }
#pragma warning restore 0162
                if ((ushort)(value >> (SizeOfUInt16InBits - 4)) == 0) { count += 4; value = (ushort)(value << 4); }
                if ((ushort)(value >> (SizeOfUInt16InBits - 2)) == 0) { count += 2; value = (ushort)(value << 2); }
                count -= (int)(value >> (SizeOfUInt16InBits - 1));
                return count;
            }
        }

        /// <summary>
        /// Returns true if <paramref name="value"/> has only a single bit set and false otherwise
        /// </summary>
        public static bool HasSingleOneBit(ushort value) => (value & unchecked((ushort)(value - 1))) == 0 && value != 0;

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" left by <paramref name="positions"/> bit positions. This is similar
        /// to shifting left, except that bits shifted off the high end reenter on the low end
        /// </summary>
        [MemberFor(typeof(ushort))]
        public static ushort RotateLeft(ushort value, int positions) => unchecked((ushort)(ShiftLeft(value, positions) | ShiftRight(value, -positions)));

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" right by <paramref name="positions"/> bit positions. This is similar
        /// to shifting right, except that bits shifted off the low end reenter on the high end
        /// </summary>
        [MemberFor(typeof(ushort))]
        public static ushort RotateRight(ushort value, int positions) => unchecked((ushort)(ShiftRight(value, positions) | ShiftLeft(value, -positions)));

        /// <summary>
        /// Returns <paramref name="value"/> with the bits reversed
        /// </summary>
        [MemberFor(typeof(ushort))]
        public static ushort Reverse(ushort value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/8u40-b25/java/lang/Long.java#Long.reverse%28short%29

            // the general approach here is to exchange pairs of bits, then sets of 4 bits, then 8, until all are exchanged. Rather than using the
            // same methodology for all exchanges we can use a shortcut to do the last two exchanges in one pass
            
            unchecked
            {
                // initial exchanges:

                // 0x5555 has all even bits set: exchanges every even bit with the following odd bit
                value = Or(ShiftLeft(And(value, (ushort)0x5555555555555555), 1), And(ShiftRight(value, 1), (ushort)0x5555555555555555));

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(ushort) > 1)
                {
                    // 0x3333 follows the pattern 1100: exchanges every even pair of bits with the following odd pair of bits
                    value = Or(ShiftLeft(And(value, (ushort)0x3333333333333333), 2), And(ShiftRight(value, 2), (ushort)0x3333333333333333));
                }
                if (sizeof(ushort) > 2)
                {
                    // 0xf0f0 follows the pattern 11110000: exchanges every even set of 4 bits with the following odd set of 4 bits
                    value = Or(ShiftLeft(And(value, (ushort)0x0f0f0f0f0f0f0f0f), 4), And(ShiftRight(value, 4), (ushort)0x0f0f0f0f0f0f0f0f));
                }
                if (sizeof(ushort) > 4)
                {
                    // 0x00ff follows the pattern 0000000011111111: exchanges adjacent sets of 8 bits
                    value = Or(ShiftLeft(And(value, (ushort)0x00ff00ff00ff00ff), 8), And(ShiftRight(value, 8), (ushort)0x00ff00ff00ff00ff));
                }

                // final exchanges:

                // used to exchange the top and bottom fourths
                const int LongShiftValue = 3 * (SizeOfUInt16InBits / 4);
                // used to exchange the 2nd and 3rd fourths
                const int ShortShiftValue = SizeOfUInt16InBits / 4;
                // used to isolate the 2nd and 3rd fourths
                const ushort SecondFourthMask = (ushort)(
                    sizeof(ushort) == 1 ? 0b1100
                        : sizeof(ushort) == 2 ? 0b11110000
                        : sizeof(ushort) == 4 ? 0b1111111100000000
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
        /// Returns <paramref name="value"/> with the bytes reversed
        /// </summary>
        [MemberFor(typeof(ushort))]
        public static ushort ReverseBytes(ushort value)
        {
            var result = Or(ShiftLeft(value, SizeOfInt64InBits - 8), ShiftRight(value, SizeOfInt64InBits - 8));
            
            unchecked
            {
                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(ushort) > 2)
                {
                    result = Or(
                        result,
                        Or(
                            And(ShiftLeft(value, SizeOfInt64InBits - 24), (ushort)((ushort)0xff << (SizeOfInt64InBits - 16))),
                            And(ShiftRight(value, SizeOfInt64InBits - 24), (ushort)0xff00)
                        )
                    );
                }
                if (sizeof(ushort) > 4)
                {
                    result = Or(
                        result,
                        Or(
                            And(ShiftLeft(value, SizeOfInt64InBits - 40), (ushort)((ushort)0xff << (SizeOfInt64InBits - 24))),
                            And(ShiftRight(value, SizeOfInt64InBits - 40), (ushort)0xff0000)
                        )
                    );
                    result = Or(
                        result,
                        Or(
                            And(ShiftLeft(value, SizeOfInt64InBits - 56), (ushort)((ushort)0xff << (SizeOfInt64InBits - 32))),
                            And(ShiftRight(value, SizeOfInt64InBits - 56), (ushort)0xff000000)
                        )
                    );
                }
#pragma warning restore 0162
            }

            return result;
        }

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITHOUT leading zeros
        /// </summary>
        [MemberFor(typeof(ushort))]
        public static string ToShortBinaryString(ushort value) => ToShortBinaryString(unchecked((short)value));

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITH ALL leading zeros
        /// </summary>
        public static string ToLongBinaryString(ushort value) => ToShortBinaryString(value).PadLeft(SizeOfUInt16InBits, '0');
        
        
    }
}
