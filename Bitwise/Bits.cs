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

        // END MEMBERS
    }
}
