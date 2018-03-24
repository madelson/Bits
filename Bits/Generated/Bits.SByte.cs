//
// AUTO-GENERATED
//
using System;
using System.Collections.Generic;
using System.Text;

namespace Medallion
{
    /// <summary>
    /// Provides implementations of various bitwise operations on primitive numeric types
    /// </summary>
    public static partial class Bits
    {
        /// <summary>
        /// Size of the <see cref="sbyte"/> type in bits
        /// </summary>
        internal const int SizeOfSByteInBits = sizeof(sbyte) * 8;

        /// <summary>
        /// The native shift operator on <see cref="sbyte"/> converts to <see cref="int"/> before shifting. This method performs
        /// a shift purely within the confines of the <see cref="sbyte"/> data type
        /// </summary>
        [MemberFor(typeof(sbyte))]
        public static sbyte ShiftLeft(sbyte value, int positions) => unchecked((sbyte)(value << (positions & ((sizeof(sbyte) * 8) - 1))));

        /// <summary>
        /// The native shift operator on <see cref="sbyte"/> converts to <see cref="int"/> before shifting. This method performs
        /// a shift purely within the confines of the <see cref="sbyte"/> data type
        /// </summary>
        [MemberFor(typeof(sbyte))]
        public static sbyte ShiftRight(sbyte value, int positions) => unchecked((sbyte)(value >> (positions & ((sizeof(sbyte) * 8) - 1))));

        /// <summary>As the native operator, but returns <see cref="sbyte"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(sbyte))]
        public static sbyte And(sbyte a, sbyte b) => unchecked((sbyte)(a & b));

        /// <summary>As the native operator, but returns <see cref="sbyte"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(sbyte))]
        public static sbyte Or(sbyte a, sbyte b) => unchecked((sbyte)(a | b));

        /// <summary>As the native operator, but returns <see cref="sbyte"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(sbyte))]
        public static sbyte Xor(sbyte a, sbyte b) => unchecked((sbyte)(a ^ b));

        /// <summary>As the native operator, but returns <see cref="sbyte"/> instead of <see cref="int"/></summary>
        [MemberFor(typeof(sbyte))]
        public static sbyte Not(sbyte value) => unchecked((sbyte)~value);

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this sbyte value, sbyte flags) => (value & flags) != 0;

        /// <summary>
        /// Determines whether <paramref name="value"/> has all of the bits set that are set in <paramref name="flags"/>
        /// </summary>
        public static bool HasAllFlags(this sbyte value, sbyte flags) => (value & flags) == flags;

        /// <summary>
        /// Determines whether the <paramref name="index"/>th bit is set in <paramref name="value"/>
        /// </summary>
        public static bool GetBit(this sbyte value, int index)
        {
            if ((index & ~(SizeOfSByteInBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return value.HasAnyFlag((sbyte)(((sbyte)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit set
        /// </summary>
        public static sbyte SetBit(this sbyte value, int index)
        {
            if ((index & ~(SizeOfSByteInBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (sbyte)(value | (sbyte)(((sbyte)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit cleared
        /// </summary>
        public static sbyte ClearBit(this sbyte value, int index)
        {
            if ((index & ~(SizeOfSByteInBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (sbyte)(value & unchecked((sbyte)~(((sbyte)1) << index)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static sbyte FlipBit(this sbyte value, int index)
        {
            if ((index & ~(SizeOfSByteInBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (sbyte)(value ^ (sbyte)(((sbyte)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the least significant set bit cleared
        /// </summary>
        public static sbyte ClearLeastSignificantOneBit(sbyte value) => (sbyte)(value & unchecked(value - 1));
        
        /// <summary>
        /// Return s<paramref name="value"/> with the least significant zero bit set
        /// </summary>
        public static sbyte SetLeastSignificantZeroBit(sbyte value) => unchecked((sbyte)(value | (sbyte)(value + 1)));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits less significant than the least significant set bit will be set.
        /// If <paramref name="value"/> is zero then all bits are trailing zero bits so the returned value will have all bits set
        /// </summary>
        public static sbyte SetTrailingZeroBits(sbyte value) => unchecked((sbyte)(value | (sbyte)(value - 1)));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static sbyte IsolateLeastSignificantOneBit(sbyte value) => (sbyte)(value & unchecked((sbyte)0 - value));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        public static sbyte IsolateMostSignificantOneBit(sbyte value) => unchecked((sbyte)IsolateMostSignificantOneBit(ToUnsigned(value)));

        /// <summary>
        /// Returns the number of set bits in <paramref name="value"/>
        /// </summary>
        public static int BitCount(sbyte value) => BitCount(ToUnsigned(value));

        /// <summary>
        /// Returns the number of zero bits following the least-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(sbyte)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="sbyte"/> data type
        /// </summary>
        public static int TrailingZeroBitCount(sbyte value) => TrailingZeroBitCount(ToUnsigned(value));

        /// <summary>
        /// Returns the number of zero bits preceding the most-significant one-bit in the binary representation of <paramref name="value"/>
        /// (as returned by <see cref="Bits.ToLongBinaryString(sbyte)"/>). If <paramref name="value"/> is zero, returns the number of bits
        /// in the <see cref="sbyte"/> data type
        /// </summary>
        public static int LeadingZeroBitCount(sbyte value) => LeadingZeroBitCount(ToUnsigned(value));

        /// <summary>
        /// Returns true if <paramref name="value"/> has only a single bit set and false otherwise
        /// </summary>
        public static bool HasSingleOneBit(sbyte value) => (value & unchecked((sbyte)(value - 1))) == 0 && value != 0;

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" left by <paramref name="positions"/> bit positions. This is similar
        /// to shifting left, except that bits shifted off the high end reenter on the low end
        /// </summary>
        public static sbyte RotateLeft(sbyte value, int positions) => unchecked((sbyte)RotateLeft(ToUnsigned(value), positions));

        /// <summary>
        /// Returns <paramref name="value"/> "rotated" right by <paramref name="positions"/> bit positions. This is similar
        /// to shifting right, except that bits shifted off the low end reenter on the high end
        /// </summary>
        public static sbyte RotateRight(sbyte value, int positions) => unchecked((sbyte)RotateRight(ToUnsigned(value), positions));

        /// <summary>
        /// Returns <paramref name="value"/> with the bits reversed
        /// </summary>
        public static sbyte Reverse(sbyte value) => unchecked((sbyte)Reverse(ToUnsigned(value)));

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITH ALL leading zeros
        /// </summary>
        public static string ToLongBinaryString(sbyte value) => ToShortBinaryString(value).PadLeft(SizeOfSByteInBits, '0');
        
        
    }
}
