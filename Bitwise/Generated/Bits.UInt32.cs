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
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit cleared
        /// </summary>
        public static uint ClearBit(this uint value, int index)
        {
            if ((index & ~(SizeOfUInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (uint)(value & unchecked((uint)~(((uint)1) << index)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static uint FlipBit(this uint value, int index)
        {
            if ((index & ~(SizeOfUInt32InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (uint)(value ^ (uint)(((uint)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the least significant bit cleared
        /// </summary>
        public static uint ClearLeastSignificantBit(uint value) => (uint)(value & unchecked(value - 1));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant set bit
        /// </summary>
        public static uint IsolateLeastSignificantSetBit(uint value) => (uint)(value & unchecked((uint)0 - value));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant set bit
        /// </summary>
        [MemberFor(typeof(uint))]
        public static uint IsolateMostSignificantSetBit(uint value)
        {
            // the idea here is to steadily set all bits less significant than the most significant bit,
            // and then follow up by clearing them all. See https://stackoverflow.com/questions/28846601/java-integer-highestonebit-in-c-sharp

            value = (uint)(value | (value >> 1));
            value = (uint)(value | (value >> 2));
            value = (uint)(value | (value >> 4));

            // to simplify codegen for smaller integral types, we fork on sizeof().
            // The compiler will remove these branches so that no additional inefficiency
            // is incurred
#pragma warning disable 0162
            if (sizeof(uint) > 1)
            {
                value = (uint)(value | (value >> 8));
                if (sizeof(uint) > 2)
                {
                    value = (uint)(value | (value >> 16));
                    if (sizeof(uint) > 4)
                    {
                        value = (uint)(value | (value >> 32));
                    }
                }
            }
#pragma warning restore 0162

            return (uint)(value - (uint)(value >> 1));
        }

        
    }
}
