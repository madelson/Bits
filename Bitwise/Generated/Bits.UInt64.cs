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
        /// Size of the <see cref="ulong"/> type in bits
        /// </summary>
        internal const int SizeOfUInt64InBits = sizeof(ulong) * 8;

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
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static ulong FlipBit(this ulong value, int index)
        {
            if ((index & ~(SizeOfUInt64InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ulong)(value ^ (ulong)(((ulong)1) << index));
        }

        
    }
}
