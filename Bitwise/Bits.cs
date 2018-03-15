using System;
using System.Collections.Generic;
using System.Text;

namespace Bitwise
{
    public static class Bits
    {
        private const int SizeOfInt64InBits = 64;

        public static bool HasAnyFlag(this long value, long flags) => (value & flags) != 0;
    }
}
