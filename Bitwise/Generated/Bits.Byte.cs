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

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static byte IsolateLeastSignificantSetBit(byte value) => (byte)(value & unchecked((byte)0 - value));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        [MemberFor(typeof(byte))]
        public static byte IsolateMostSignificantSetBit(byte value)
        {
            // the idea here is to steadily set all bits less significant than the most significant bit,
            // and then follow up by clearing them all. See https://stackoverflow.com/questions/28846601/java-integer-highestonebit-in-c-sharp

            value = (byte)(value | (value >> 1));
            value = (byte)(value | (value >> 2));
            value = (byte)(value | (value >> 4));

            // to simplify codegen for smaller integral types, we fork on sizeof().
            // The compiler will remove these branches so that no additional inefficiency
            // is incurred
#pragma warning disable 0162
            if (sizeof(byte) > 1)
            {
                value = (byte)(value | (value >> 8));
                if (sizeof(byte) > 2)
                {
                    value = (byte)(value | (value >> 16));
                    if (sizeof(byte) > 4)
                    {
                        value = (byte)(value | (value >> 32));
                    }
                }
            }
#pragma warning restore 0162

            return (byte)(value - (byte)(value >> 1));
        }

        /// <summary>
        /// Returns the number of set bits in <paramref name="value"/>
        /// </summary>
        [MemberFor(typeof(byte))]
        public static int BitCount(byte value)
        {
            // based on http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/6-b14/java/lang/Long.java#Long.bitCount%28sbyte%29
            // also described here: http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetParallel

            unchecked
            {
                value = (byte)(value - (byte)((byte)(value >> 1) & (byte)0x5555555555555555));
                value = (byte)((byte)(value & (byte)0x3333333333333333) + (byte)((byte)(value >> 2) & (byte)0x3333333333333333));
                value = (byte)((byte)(value + (byte)(value >> 4)) & (byte)0x0f0f0f0f0f0f0f0f);

                // to simplify codegen for smaller integral types, we fork on sizeof().
                // The compiler will remove these branches so that no additional inefficiency
                // is incurred
#pragma warning disable 0162
                if (sizeof(byte) > 1)
                {
                    value = (byte)(value + (byte)(value >> 8));
                    if (sizeof(byte) > 2)
                    {
                        value = (byte)(value + (byte)(value >> 16));
                        if (sizeof(byte) > 4)
                        {
                            value = (byte)(value + (byte)(value >> 32));

                            // byte
                            return (int)(value & (byte)0b1111111);
                        }

                        // uint
                        return (int)(value & (byte)0b111111);
                    }

                    // ushort
                    return (int)(value & (byte)0b11111);
                }

                // byte
                return (int)(value & (byte)0b1111);
#pragma warning restore 0162
            }
        }

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITHOUT leading zeros
        /// </summary>
        [MemberFor(typeof(byte))]
        public static string ToShortBinaryString(byte value) => ToShortBinaryString(unchecked((sbyte)value));

        /// <summary>
        /// Returns the binary representation of <paramref name="value"/> WITH ALL leading zeros
        /// </summary>
        public static string ToLongBinaryString(byte value) => ToShortBinaryString(value).PadLeft(SizeOfByteInBits, '0');
        
        
    }
}
