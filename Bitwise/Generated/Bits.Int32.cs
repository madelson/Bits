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
        /// Size of the <see cref="int"/> type in bits
        /// </summary>
        internal const int SizeOfInt32InBits = sizeof(int) * 8;

        /// <summary>
        /// The native shift operator on <see cref="int"/> converts to <see cref="int"/> before shifting. This method performs
        /// a shift purely within the confines of the <see cref="int"/> data type
        /// </summary>
        [MemberFor(typeof(int))]
        public static int ShiftLeft(int value, int positions) => unchecked((int)(value << (positions & ((sizeof(int) * 8) - 1))));

        /// <summary>
        /// The native shift operator on <see cref="int"/> converts to <see cref="int"/> before shifting. This method performs
        /// a shift purely within the confines of the <see cref="int"/> data type
        /// </summary>
        [MemberFor(typeof(int))]
        public static int ShiftRight(int value, int positions) => unchecked((int)(value >> (positions & ((sizeof(int) * 8) - 1))));

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this int value, int flags) => (value & flags) != 0;

        /// <summary>
        /// Determines whether <paramref name="value"/> has all of the bits set that are set in <paramref name="flags"/>
        /// </summary>
        public static bool HasAllFlags(this int value, int flags) => (value & flags) == flags;

        /// <summary>
        /// Determines whether the <paramref name="index"/>th bit is set in <paramref name="value"/>
        /// </summary>
        public static bool GetBit(this int value, int index)
        {
            if ((index & ~(SizeOfInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return value.HasAnyFlag((int)(((int)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit set
        /// </summary>
        public static int SetBit(this int value, int index)
        {
            if ((index & ~(SizeOfInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (int)(value | (int)(((int)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit cleared
        /// </summary>
        public static int ClearBit(this int value, int index)
        {
            if ((index & ~(SizeOfInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (int)(value & unchecked((int)~(((int)1) << index)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static int FlipBit(this int value, int index)
        {
            if ((index & ~(SizeOfInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (int)(value ^ (int)(((int)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the least significant bit cleared
        /// </summary>
        public static int ClearLeastSignificantBit(int value) => (int)(value & unchecked(value - 1));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static int IsolateLeastSignificantSetBit(int value) => (int)(value & unchecked((int)0 - value));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        public static int IsolateMostSignificantSetBit(int value) => unchecked((int)IsolateMostSignificantSetBit(ToUnsigned(value)));

        /// <summary>
        /// Returns the number of set bits in <paramref name="value"/>
        /// </summary>
        public static int BitCount(int value) => BitCount(ToUnsigned(value));

        /// <summary>
        /// Returns the number of zero bits following the least-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(int)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="int"/> data type
        /// </summary>
        public static int TrailingZeroBitCount(int value) => TrailingZeroBitCount(ToUnsigned(value));

        /// <summary>
        /// Returns the number of zero bits preceding the most-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(int)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="int"/> data type
        /// </summary>
        public static int LeadingZeroBitCount(int value) => LeadingZeroBitCount(ToUnsigned(value));

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" left by <paramref name="positions"/> bit positions. This is similar
        /// to shifting left, except that bits shifted off the high end reenter on the low end
        /// </summary>
        public static int RotateLeft(int value, int positions) => unchecked((int)RotateLeft(ToUnsigned(value), positions));

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" right by <paramref name="positions"/> bit positions. This is similar
        /// to shifting right, except that bits shifted off the low end reenter on the high end
        /// </summary>
        public static int RotateRight(int value, int positions) => unchecked((int)RotateRight(ToUnsigned(value), positions));

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITHOUT leading zeros
        /// </summary>
        [MemberFor(typeof(int))] // Convert.ToString(#, base) is defined for byte rather than for int
        public static string ToShortBinaryString(int value) => Convert.ToString(ToUnsigned(value), toBase: 2);

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITH ALL leading zeros
        /// </summary>
        public static string ToLongBinaryString(int value) => ToShortBinaryString(value).PadLeft(SizeOfInt32InBits, '0');
        
        
    }
}
