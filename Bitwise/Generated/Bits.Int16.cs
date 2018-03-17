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
        /// Size of the <see cref="short"/> type in bits
        /// </summary>
        internal const int SizeOfInt16InBits = sizeof(short) * 8;

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this short value, short flags) => (value & flags) != 0;

        /// <summary>
        /// Determines whether <paramref name="value"/> has all of the bits set that are set in <paramref name="flags"/>
        /// </summary>
        public static bool HasAllFlags(this short value, short flags) => (value & flags) == flags;

        /// <summary>
        /// Determines whether the <paramref name="index"/>th bit is set in <paramref name="value"/>
        /// </summary>
        public static bool GetBit(this short value, int index)
        {
            if ((index & ~(SizeOfInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return value.HasAnyFlag((short)(((short)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit set
        /// </summary>
        public static short SetBit(this short value, int index)
        {
            if ((index & ~(SizeOfInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (short)(value | (short)(((short)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit cleared
        /// </summary>
        public static short ClearBit(this short value, int index)
        {
            if ((index & ~(SizeOfInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (short)(value & unchecked((short)~(((short)1) << index)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static short FlipBit(this short value, int index)
        {
            if ((index & ~(SizeOfInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (short)(value ^ (short)(((short)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the least significant bit cleared
        /// </summary>
        public static short ClearLeastSignificantBit(short value) => (short)(value & unchecked(value - 1));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static short IsolateLeastSignificantSetBit(short value) => (short)(value & unchecked((short)0 - value));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        public static short IsolateMostSignificantSetBit(short value) => unchecked((short)IsolateMostSignificantSetBit(ToUnsigned(value)));

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITHOUT leading zeros
        /// </summary>
        public static string ToShortBinaryString(short value) => Convert.ToString(value, toBase: 2);

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITH ALL leading zeros
        /// </summary>
        public static string ToLongBinaryString(short value) => ToShortBinaryString(value).PadLeft(SizeOfInt16InBits, '0');
        
        
    }
}
