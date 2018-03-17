using System;
using System.Collections.Generic;
using System.Text;

namespace Bitwise
{
    public static partial class Bits
    {
        /// <summary>
        /// Size of the <see cref="long"/> type in bits
        /// </summary>
        internal const int SizeOfInt64InBits = sizeof(long) * 8;

        /// <summary>
        /// Size of the <see cref="ulong"/> type in bits
        /// </summary>
        [MemberFor(typeof(ulong))] // can't be auto-generated because ulong members may need this directly
        internal const int SizeOfUInt64InBits = sizeof(ulong) * 8;

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this long value, long flags) => (value & flags) != 0;

        /// <summary>
        /// Determines whether <paramref name="value"/> has all of the bits set that are set in <paramref name="flags"/>
        /// </summary>
        public static bool HasAllFlags(this long value, long flags) => (value & flags) == flags;

        /// <summary>
        /// Determines whether the <paramref name="index"/>th bit is set in <paramref name="value"/>
        /// </summary>
        public static bool GetBit(this long value, int index)
        {
            if ((index & ~(SizeOfInt64InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return value.HasAnyFlag((long)(((long)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit set
        /// </summary>
        public static long SetBit(this long value, int index)
        {
            if ((index & ~(SizeOfInt64InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (long)(value | (long)(((long)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit cleared
        /// </summary>
        public static long ClearBit(this long value, int index)
        {
            if ((index & ~(SizeOfInt64InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (long)(value & unchecked((long)~(((long)1) << index)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static long FlipBit(this long value, int index)
        {
            if ((index & ~(SizeOfInt64InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (long)(value ^ (long)(((long)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the least significant bit cleared
        /// </summary>
        public static long ClearLeastSignificantBit(long value) => (long)(value & unchecked(value - 1));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static long IsolateLeastSignificantSetBit(long value) => (long)(value & unchecked((long)0 - value));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        [MemberFor(typeof(ulong))]
        public static ulong IsolateMostSignificantSetBit(ulong value)
        {
            // the idea here is to steadily set all bits less significant than the most significant bit,
            // and then follow up by clearing them all. See https://stackoverflow.com/questions/28846601/java-integer-highestonebit-in-c-sharp

            value = (ulong)(value | (value >> 1));
            value = (ulong)(value | (value >> 2));
            value = (ulong)(value | (value >> 4));

            // to simplify codegen for smaller integral types, we fork on sizeof().
            // The compiler will remove these branches so that no additional inefficiency
            // is incurred
#pragma warning disable 0162
            if (sizeof(ulong) > 1)
            {
                value = (ulong)(value | (value >> 8));
                if (sizeof(ulong) > 2)
                {
                    value = (ulong)(value | (value >> 16));
                    if (sizeof(ulong) > 4)
                    {
                        value = (ulong)(value | (value >> 32));
                    }
                }
            }
#pragma warning restore 0162

            return (ulong)(value - (ulong)(value >> 1));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        public static long IsolateMostSignificantSetBit(long value) => unchecked((long)IsolateMostSignificantSetBit(ToUnsigned(value)));

        /// <summary>
        /// Returns the number of set bits in <paramref name="value"/>
        /// </summary>
        [MemberFor(typeof(ulong))]
        public static int BitCount(ulong value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/6-b14/java/lang/Long.java#Long.bitCount%28long%29
            // also described here: http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetParallel

            unchecked
            {
                value = (ulong)(value - (ulong)((ulong)(value >> 1) & (ulong)0x5555555555555555));
                value = (ulong)((ulong)(value & (ulong)0x3333333333333333) + (ulong)((ulong)(value >> 2) & (ulong)0x3333333333333333));
                value = (ulong)((ulong)(value + (ulong)(value >> 4)) & (ulong)0x0f0f0f0f0f0f0f0f);

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(ulong) > 1)
                {
                    value = (ulong)(value + (ulong)(value >> 8));
                    if (sizeof(ulong) > 2)
                    {
                        value = (ulong)(value + (ulong)(value >> 16));
                        if (sizeof(ulong) > 4)
                        {
                            value = (ulong)(value + (ulong)(value >> 32));

                            // ulong
                            return (int)(value & (ulong)0b1111111);
                        }

                        // uint
                        return (int)(value & (ulong)0b111111);
                    }

                    // ushort
                    return (int)(value & (ulong)0b11111);
                }

                // byte
                return (int)(value & (ulong)0b1111);
#pragma warning restore 0162
            }
        }

        /// <summary>
        /// Returns the number of set bits in <paramref name="value"/>
        /// </summary>
        public static int BitCount(long value) => BitCount(ToUnsigned(value));

        /// <summary>
        /// Returns the number of zero bits following the least-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(long)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="long"/> data type
        /// </summary>
        [MemberFor(typeof(ulong))]
        public static int TrailingZeroBitCount(ulong value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/8u40-b25/java/lang/Integer.java#Integer.numberOfTrailingZeros%28int%29
 
            if (value == (ulong)0) { return SizeOfUInt64InBits; }

            unchecked
            {
                // the algorithm here is really very simple. We start by assuming that we have
                // N-1 trailing zeros (N being special cased above). We then proceed to cut off
                // half of the bits remaining with a left-shift. If that leaves a non-zero remainder,
                // then we know that the lower half has a one and thus our assumption of the answer
                // drops to at most N_lower_half - 1. We only "keep" the shift if this was the case, the
                // result of which is that the bits needed to calculate the rest of the answer are now
                // the top bits

                var count = SizeOfUInt64InBits - 1;
                ulong y;

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(ulong) > 4)
                {
                    y = (ulong)(value << 32);
                    if (y != 0) { count -= 32; value = y; }
                }

                if (sizeof(ulong) > 2)
                {
                    y = (ulong)(value << 16);
                    if (y != 0) { count -= 16; value = y; }
                }

                if (sizeof(ulong) > 1)
                {
                    y = (ulong)(value << 8);
                    if (y != 0) { count -= 8; value = y; }
                }
#pragma warning restore 0162

                y = (ulong)(value << 4);
                if (y != 0) { count -= 4; value = y; }

                y = (ulong)(value << 2);
                if (y != 0) { count -= 2; value = y; }

                return (int)(count - (int)((ulong)(value << 1) >> (SizeOfUInt64InBits - 1)));
            }
        }

        /// <summary>
        /// Returns the number of zero bits following the least-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(long)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="long"/> data type
        /// </summary>
        public static int TrailingZeroBitCount(long value) => TrailingZeroBitCount(ToUnsigned(value));

        /// <summary>
        /// Returns the number of zero bits preceding the most-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(long)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="long"/> data type
        /// </summary>
        [MemberFor(typeof(ulong))]
        public static int LeadingZeroBitCount(ulong value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/8u40-b25/java/lang/Integer.java#Integer.numberOfLeadingZeros%28int%29

            if (value == (ulong)0) { return SizeOfUInt64InBits; }

            unchecked
            {
                var count = 1;

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(ulong) > 4)
                {
                    if ((ulong)(value >> (SizeOfUInt64InBits - 32)) == 0) { count += 32; value = (ulong)(value << 32); }
                }
                if (sizeof(ulong) > 2)
                {
                    if ((ulong)(value >> (SizeOfUInt64InBits - 16)) == 0) { count += 16; value = (ulong)(value << 16); }
                }
                if (sizeof(ulong) > 1)
                {
                    if ((ulong)(value >> (SizeOfUInt64InBits - 8)) == 0) { count += 8; value = (ulong)(value << 8); }
                }
#pragma warning restore 0162
                if ((ulong)(value >> (SizeOfUInt64InBits - 4)) == 0) { count += 4; value = (ulong)(value << 4); }
                if ((ulong)(value >> (SizeOfUInt64InBits - 2)) == 0) { count += 2; value = (ulong)(value << 2); }
                count -= (int)(value >> (SizeOfUInt64InBits - 1));
                return count;
            }
        }

        /// <summary>
        /// Returns the number of zero bits preceding the most-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(long)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="long"/> data type
        /// </summary>
        public static int LeadingZeroBitCount(long value) => LeadingZeroBitCount(ToUnsigned(value));

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITHOUT leading zeros
        /// </summary>
        public static string ToShortBinaryString(long value) => Convert.ToString(value, toBase: 2);

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITHOUT leading zeros
        /// </summary>
        [MemberFor(typeof(ulong))]
        public static string ToShortBinaryString(ulong value) => ToShortBinaryString(unchecked((long)value));

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITHOUT leading zeros
        /// </summary>
        [MemberFor(typeof(sbyte))] // Convert.ToString(#, base) is defined for byte rather than for sbyte
        public static string ToShortBinaryString(sbyte value) => Convert.ToString(ToUnsigned(value), toBase: 2);

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITH ALL leading zeros
        /// </summary>
        public static string ToLongBinaryString(long value) => ToShortBinaryString(value).PadLeft(SizeOfInt64InBits, '0');
        
        // END MEMBERS
    }
}
