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
        /// Size of the <see cref="sbyte"/> type in bits
        /// </summary>
        private const int SizeOfSByteInBits = sizeof(sbyte) * 8;

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this sbyte value, sbyte flags) => (value & flags) != 0;

        
    }
}
