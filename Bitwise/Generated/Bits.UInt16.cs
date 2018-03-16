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
        /// Size of the <see cref="ushort"/> type in bits
        /// </summary>
        internal const int SizeOfUInt16InBits = sizeof(ushort) * 8;

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this ushort value, ushort flags) => (value & flags) != 0;

        /// <summary>
        /// Determines whether <paramref name="value"/> has all of the bits set that are set in <paramref name="flags"/>
        /// </summary>
        public static bool HasAllFlags(this ushort value, ushort flags) => (value & flags) == flags;

        /// <summary>
        /// Determines whether the <paramref name="index"/>th bit is set in <paramref name="value"/>
        /// </summary>
        public static bool GetBit(this ushort value, int index)
        {
            if ((index & ~(SizeOfUInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return value.HasAnyFlag((ushort)(((ushort)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit set
        /// </summary>
        public static ushort SetBit(this ushort value, int index)
        {
            if ((index & ~(SizeOfUInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ushort)(value | (ushort)(((ushort)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit cleared
        /// </summary>
        public static ushort ClearBit(this ushort value, int index)
        {
            if ((index & ~(SizeOfUInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ushort)(value & unchecked((ushort)~(((ushort)1) << index)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static ushort FlipBit(this ushort value, int index)
        {
            if ((index & ~(SizeOfUInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ushort)(value ^ (ushort)(((ushort)1) << index));
        }

        
    }
}
