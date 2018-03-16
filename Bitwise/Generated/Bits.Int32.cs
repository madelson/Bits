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

        
    }
}
