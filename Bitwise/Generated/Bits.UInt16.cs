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
        private const int SizeOfUInt16InBits = sizeof(ushort) * 8;

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this ushort value, ushort flags) => (value & flags) != 0;

        
    }
}
