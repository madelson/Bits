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
        /// Size of the <see cref="byte"/> type in bits
        /// </summary>
        internal const int SizeOfByteInBits = sizeof(byte) * 8;

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this byte value, byte flags) => (value & flags) != 0;

        /// <summary>
        /// Determines whether <paramref name="value"/> has all of the bits set that are set in <paramref name="flags"/>
        /// </summary>
        public static bool HasAllFlags(this byte value, byte flags) => (value & flags) == flags;

        /// <summary>
        /// Determines whether the <paramref name="index"/>th bit is set in <paramref name="value"/>
        /// </summary>
        public static bool GetBit(this byte value, int index)
        {
            if ((index & ~(SizeOfByteInBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return value.HasAnyFlag((byte)(((byte)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit set
        /// </summary>
        public static byte SetBit(this byte value, int index)
        {
            if ((index & ~(SizeOfByteInBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (byte)(value | (byte)(((byte)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit cleared
        /// </summary>
        public static byte ClearBit(this byte value, int index)
        {
            if ((index & ~(SizeOfByteInBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (byte)(value & unchecked((byte)~(((byte)1) << index)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static byte FlipBit(this byte value, int index)
        {
            if ((index & ~(SizeOfByteInBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (byte)(value ^ (byte)(((byte)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the least significant bit cleared
        /// </summary>
        public static byte ClearLeastSignificantBit(byte value) => (byte)(value & unchecked(value - 1));

        
    }
}
