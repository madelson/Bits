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
        /// Size of the <see cref="uint"/> type in bits
        /// </summary>
        internal const int SizeOfUInt32InBits = sizeof(uint) * 8;

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this uint value, uint flags) => (value & flags) != 0;

        /// <summary>
        /// Determines whether <paramref name="value"/> has all of the bits set that are set in <paramref name="flags"/>
        /// </summary>
        public static bool HasAllFlags(this uint value, uint flags) => (value & flags) == flags;

        /// <summary>
        /// Determines whether the <paramref name="index"/>th bit is set in <paramref name="value"/>
        /// </summary>
        public static bool GetBit(this uint value, int index)
        {
            if ((index & ~(SizeOfUInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return value.HasAnyFlag((uint)(((uint)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit set
        /// </summary>
        public static uint SetBit(this uint value, int index)
        {
            if ((index & ~(SizeOfUInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (uint)(value | (uint)(((uint)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static uint FlipBit(this uint value, int index)
        {
            if ((index & ~(SizeOfUInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (uint)(value ^ (uint)(((uint)1) << index));
        }

        
    }
}
