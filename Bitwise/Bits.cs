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
        
        // END MEMBERS
    }
}
