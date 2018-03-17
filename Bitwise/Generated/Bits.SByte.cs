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
        /// Size of the <see cref="sbyte"/> type in bits
        /// </summary>
        internal const int SizeOfSByteInBits = sizeof(sbyte) * 8;

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
        /// Returns <paramref name="value"/> with the least significant bit cleared
        /// </summary>
        public static sbyte ClearLeastSignificantBit(sbyte value) => (sbyte)(value & unchecked(value - 1));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static sbyte IsolateLeastSignificantSetBit(sbyte value) => (sbyte)(value & unchecked((sbyte)0 - value));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        public static sbyte IsolateMostSignificantSetBit(sbyte value) => unchecked((sbyte)IsolateMostSignificantSetBit(ToUnsigned(value)));
        
        
    }
}
