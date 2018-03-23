using System;
using System.Collections.Generic;
using System.Text;

namespace Medallion
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
        /// The presence of this method simplifies codegen when using <see cref="ShiftLeft(short, int)"/> and similar
        /// </summary>
        internal static long ShiftLeft(long value, int positions) => value << positions;

        /// <summary>
        /// The presence of this method simplifies codegen when using <see cref="ShiftLeft(short, int)"/> and similar
        /// </summary>
        [MemberFor(typeof(ulong))]
        internal static ulong ShiftLeft(ulong value, int positions) => value << positions;

        /// <summary>
        /// The native shift operator on <see cref="short"/> converts to <see cref="int"/> before shifting. This method performs
        /// a shift purely within the confines of the <see cref="short"/> data type
        /// </summary>
        [MemberFor(typeof(short))]
        public static short ShiftLeft(short value, int positions) => unchecked((short)(value << (positions & ((sizeof(short) * 8) - 1))));

        /// <summary>
        /// The native shift operator on <see cref="ushort"/> converts to <see cref="int"/> before shifting. This method performs
        /// a shift purely within the confines of the <see cref="ushort"/> data type
        /// </summary>
        [MemberFor(typeof(ushort))]
        public static ushort ShiftLeft(ushort value, int positions) => unchecked((ushort)(value << (positions & ((sizeof(ushort) * 8) - 1))));

        /// <summary>
        /// The presence of this method simplifies codegen when using <see cref="ShiftRight(short, int)"/> and similar
        /// </summary>
        internal static long ShiftRight(long value, int positions) => value >> positions;

        /// <summary>
        /// The presence of this method simplifies codegen when using <see cref="ShiftRight(short, int)"/> and similar
        /// </summary>
        [MemberFor(typeof(ulong))]
        internal static ulong ShiftRight(ulong value, int positions) => value >> positions;

        /// <summary>
        /// The native shift operator on <see cref="short"/> converts to <see cref="int"/> before shifting. This method performs
        /// a shift purely within the confines of the <see cref="short"/> data type
        /// </summary>
        [MemberFor(typeof(short))]
        public static short ShiftRight(short value, int positions) => unchecked((short)(value >> (positions & ((sizeof(short) * 8) - 1))));

        /// <summary>
        /// The native shift operator on <see cref="ushort"/> converts to <see cref="int"/> before shifting. This method performs
        /// a shift purely within the confines of the <see cref="ushort"/> data type
        /// </summary>
        [MemberFor(typeof(ushort))]
        public static ushort ShiftRight(ushort value, int positions) => unchecked((ushort)(value >> (positions & ((sizeof(ushort) * 8) - 1))));

        /// <summary>Simplifies codegen</summary>
        internal static long And(long a, long b) => a & b;

        /// <summary>Simplifies codegen</summary>
        [MemberFor(typeof(ulong))]
        internal static ulong And(ulong a, ulong b) => a & b;

        /// <summary>As the native operator, but returns <see cref="short"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(short))]
        public static short And(short a, short b) => unchecked((short)(a & b));

        /// <summary>As the native operator, but returns <see cref="ushort"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(ushort))]
        public static ushort And(ushort a, ushort b) => unchecked((ushort)(a & b));

        /// <summary>Simplifies codegen</summary>
        internal static long Or(long a, long b) => a | b;

        /// <summary>Simplifies codegen</summary>
        [MemberFor(typeof(ulong))]
        internal static ulong Or(ulong a, ulong b) => a | b;

        /// <summary>As the native operator, but returns <see cref="short"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(short))]
        public static short Or(short a, short b) => unchecked((short)(a | b));

        /// <summary>As the native operator, but returns <see cref="ushort"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(ushort))]
        public static ushort Or(ushort a, ushort b) => unchecked((ushort)(a | b));

        /// <summary>Simplifies codegen</summary>
        internal static long Xor(long a, long b) => a ^ b;

        /// <summary>Simplifies codegen</summary>
        [MemberFor(typeof(ulong))]
        internal static ulong Xor(ulong a, ulong b) => a ^ b;

        /// <summary>As the native operator, but returns <see cref="short"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(short))]
        public static short Xor(short a, short b) => unchecked((short)(a ^ b));

        /// <summary>As the native operator, but returns <see cref="ushort"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(ushort))]
        public static ushort Xor(ushort a, ushort b) => unchecked((ushort)(a ^ b));

        /// <summary>Simplifies codegen</summary>
        internal static long Not(long value) => ~value;

        /// <summary>Simplifies codegen</summary>
        [MemberFor(typeof(ulong))]
        internal static ulong Not(ulong value) => ~value;

        /// <summary>As the native operator, but returns <see cref="short"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(short))]
        public static short Not(short value) => unchecked((short)~value);

        /// <summary>As the native operator, but returns <see cref="ushort"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(ushort))]
        public static ushort Not(ushort value) => unchecked((ushort)~value);

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
        /// Returns <paramref name="value"/> with the least significant set bit cleared
        /// </summary>
        public static long ClearLeastSignificantOneBit(long value) => (long)(value & unchecked(value - 1));
        
        /// <summary>
        /// Return s<paramref name="value"/> with the least significant zero bit set
        /// </summary>
        public static long SetLeastSignificantZeroBit(long value) => unchecked((long)(value | (long)(value + 1)));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits less significant than the least significant set bit will be set.
        /// If <paramref name="value"/> is zero then all bits are trailing zero bits so the returned value will have all bits set
        /// </summary>
        public static long SetTrailingZeroBits(long value) => unchecked((long)(value | (long)(value - 1)));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static long IsolateLeastSignificantOneBit(long value) => (long)(value & unchecked((long)0 - value));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        [MemberFor(typeof(ulong))]
        public static ulong IsolateMostSignificantOneBit(ulong value)
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

            return unchecked((ulong)(value - (ulong)(value >> 1)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        public static long IsolateMostSignificantOneBit(long value) => unchecked((long)IsolateMostSignificantOneBit(ToUnsigned(value)));

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
        /// Returns true if <paramref name="value"/> has only a single bit set and false otherwise
        /// </summary>
        public static bool HasSingleOneBit(long value) => (value & unchecked((long)(value - 1))) == 0 && value != 0;

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" left by <paramref name="positions"/> bit positions. This is similar
        /// to shifting left, except that bits shifted off the high end reenter on the low end
        /// </summary>
        [MemberFor(typeof(ulong))]
        public static ulong RotateLeft(ulong value, int positions) => unchecked((ulong)(ShiftLeft(value, positions) | ShiftRight(value, -positions)));

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" left by <paramref name="positions"/> bit positions. This is similar
        /// to shifting left, except that bits shifted off the high end reenter on the low end
        /// </summary>
        public static long RotateLeft(long value, int positions) => unchecked((long)RotateLeft(ToUnsigned(value), positions));

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" right by <paramref name="positions"/> bit positions. This is similar
        /// to shifting right, except that bits shifted off the low end reenter on the high end
        /// </summary>
        [MemberFor(typeof(ulong))]
        public static ulong RotateRight(ulong value, int positions) => unchecked((ulong)(ShiftRight(value, positions) | ShiftLeft(value, -positions)));

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" right by <paramref name="positions"/> bit positions. This is similar
        /// to shifting right, except that bits shifted off the low end reenter on the high end
        /// </summary>
        public static long RotateRight(long value, int positions) => unchecked((long)RotateRight(ToUnsigned(value), positions));

        /// <summary>
        /// Returns <paramref name="value"/> with the bits reversed
        /// </summary>
        [MemberFor(typeof(ulong))]
        public static ulong Reverse(ulong value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/8u40-b25/java/lang/Long.java#Long.reverse%28long%29

            // the general approach here is to exchange pairs of bits, then sets of 4 bits, then 8, until all are exchanged. Rather than using the
            // same methodology for all exchanges we can use a shortcut to do the last two exchanges in one pass
            
            unchecked
            {
                // initial exchanges:

                // 0x5555 has all even bits set: exchanges every even bit with the following odd bit
                value = Or(ShiftLeft(And(value, (ulong)0x5555555555555555), 1), And(ShiftRight(value, 1), (ulong)0x5555555555555555));

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(ulong) > 1)
                {
                    // 0x3333 follows the pattern 1100: exchanges every even pair of bits with the following odd pair of bits
                    value = Or(ShiftLeft(And(value, (ulong)0x3333333333333333), 2), And(ShiftRight(value, 2), (ulong)0x3333333333333333));
                }
                if (sizeof(ulong) > 2)
                {
                    // 0xf0f0 follows the pattern 11110000: exchanges every even set of 4 bits with the following odd set of 4 bits
                    value = Or(ShiftLeft(And(value, (ulong)0x0f0f0f0f0f0f0f0f), 4), And(ShiftRight(value, 4), (ulong)0x0f0f0f0f0f0f0f0f));
                }
                if (sizeof(ulong) > 4)
                {
                    // 0x00ff follows the pattern 0000000011111111: exchanges adjacent sets of 8 bits
                    value = Or(ShiftLeft(And(value, (ulong)0x00ff00ff00ff00ff), 8), And(ShiftRight(value, 8), (ulong)0x00ff00ff00ff00ff));
                }

                // final exchanges:

                // used to exchange the top and bottom fourths
                const int LongShiftValue = 3 * (SizeOfUInt64InBits / 4);
                // used to exchange the 2nd and 3rd fourths
                const int ShortShiftValue = SizeOfUInt64InBits / 4;
                // used to isolate the 2nd and 3rd fourths
                const ulong SecondFourthMask = (ulong)(
                    sizeof(ulong) == 1 ? 0b1100
                        : sizeof(ulong) == 2 ? 0b11110000
                        : sizeof(ulong) == 4 ? 0b1111111100000000
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
        /// Returns <paramref name="value"/> with the bits reversed
        /// </summary>
        public static long Reverse(long value) => unchecked((long)Reverse(ToUnsigned(value)));

        /// <summary>
        /// Returns <paramref name="value"/> with the bytes reversed
        /// </summary>
        [MemberFor(typeof(ulong))]
        public static ulong ReverseBytes(ulong value)
        {
            var result = Or(ShiftLeft(value, SizeOfInt64InBits - 8), ShiftRight(value, SizeOfInt64InBits - 8));
            
            unchecked
            {
                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(ulong) > 2)
                {
                    result = Or(
                        result,
                        Or(
                            And(ShiftLeft(value, SizeOfInt64InBits - 24), (ulong)((ulong)0xff << (SizeOfInt64InBits - 16))),
                            And(ShiftRight(value, SizeOfInt64InBits - 24), (ulong)0xff00)
                        )
                    );
                }
                if (sizeof(ulong) > 4)
                {
                    result = Or(
                        result,
                        Or(
                            And(ShiftLeft(value, SizeOfInt64InBits - 40), (ulong)((ulong)0xff << (SizeOfInt64InBits - 24))),
                            And(ShiftRight(value, SizeOfInt64InBits - 40), (ulong)0xff0000)
                        )
                    );
                    result = Or(
                        result,
                        Or(
                            And(ShiftLeft(value, SizeOfInt64InBits - 56), (ulong)((ulong)0xff << (SizeOfInt64InBits - 32))),
                            And(ShiftRight(value, SizeOfInt64InBits - 56), (ulong)0xff000000)
                        )
                    );
                }
#pragma warning restore 0162
            }

            return result;
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the bytes reversed
        /// </summary>
        public static long ReverseBytes(long value) => unchecked((long)ReverseBytes(ToUnsigned(value)));

        /// <summary>no-op</summary>
        [MemberFor(typeof(byte))]
        internal static byte ReverseBytes(byte value) => value;

        /// <summary>no-op</summary>
        [MemberFor(typeof(sbyte))]
        internal static sbyte ReverseBytes(sbyte value) => value;

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
