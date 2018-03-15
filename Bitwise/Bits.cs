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
        private const int SizeOfInt64InBits = sizeof(long) * 8;

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this long value, long flags) => (value & flags) != 0;

        // END MEMBERS
    }
}
