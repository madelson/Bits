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
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this ulong value, ulong flags) => (value & flags) != 0;

        /// <summary>
        /// Determines whether <paramref name="value"/> has all of the bits set that are set in <paramref name="flags"/>
        /// </summary>
        public static bool HasAllFlags(this ulong value, ulong flags) => (value & flags) == flags;

        /// <summary>
        /// Determines whether the <paramref name="index"/>th bit is set in <paramref name="value"/>
        /// </summary>
        public static bool GetBit(this ulong value, int index)
        {
            if ((index & ~(SizeOfUInt64InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return value.HasAnyFlag((ulong)(((ulong)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit set
        /// </summary>
        public static ulong SetBit(this ulong value, int index)
        {
            if ((index & ~(SizeOfUInt64InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ulong)(value | (ulong)(((ulong)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit cleared
        /// </summary>
        public static ulong ClearBit(this ulong value, int index)
        {
            if ((index & ~(SizeOfUInt64InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ulong)(value & unchecked((ulong)~(((ulong)1) << index)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static ulong FlipBit(this ulong value, int index)
        {
            if ((index & ~(SizeOfUInt64InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ulong)(value ^ (ulong)(((ulong)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the least significant set bit cleared
        /// </summary>
        public static ulong ClearLeastSignificantOneBit(ulong value) => (ulong)(value & unchecked(value - 1));
        
        /// <summary>
        /// Return s<paramref name="value"/> with the least significant zero bit set
        /// </summary>
        public static ulong SetLeastSignificantZeroBit(ulong value) => unchecked((ulong)(value | (ulong)(value + 1)));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits less significant than the least significant set bit will be set.
        /// If <paramref name="value"/> is zero then all bits are trailing zero bits so the returned value will have all bits set
        /// </summary>
        public static ulong SetTrailingZeroBits(ulong value) => unchecked((ulong)(value | (ulong)(value - 1)));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static ulong IsolateLeastSignificantOneBit(ulong value) => (ulong)(value & unchecked((ulong)0 - value));

        /// <summary>
        /// Returns true if <paramref name="value"/> has only a single bit set and false otherwise
        /// </summary>
        public static bool HasSingleOneBit(ulong value) => (value & unchecked((ulong)(value - 1))) == 0 && value != 0;

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITH ALL leading zeros
        /// </summary>
        public static string ToLongBinaryString(ulong value) => ToShortBinaryString(value).PadLeft(SizeOfUInt64InBits, '0');
        
        
    }
}
