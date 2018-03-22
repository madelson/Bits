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
        /// Returns <paramref name="value"/> with the least significant bit cleared
        /// </summary>
        public static ulong ClearLeastSignificantBit(ulong value) => (ulong)(value & unchecked(value - 1));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static ulong IsolateLeastSignificantSetBit(ulong value) => (ulong)(value & unchecked((ulong)0 - value));

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITH ALL leading zeros
        /// </summary>
        public static string ToLongBinaryString(ulong value) => ToShortBinaryString(value).PadLeft(SizeOfUInt64InBits, '0');
        
        
    }
}