using System;
using System.Collections.Generic;
using System.Text;

namespace Medallion
{
    public static partial class Bits
    {
        private static void ThrowIndexOutOfRange() => throw new IndexOutOfRangeException();

        internal static ulong ToUnsigned(long value) => unchecked((ulong)value);
        internal static uint ToUnsigned(int value) => unchecked((uint)value);
        internal static ushort ToUnsigned(short value) => unchecked((ushort)value);
        internal static byte ToUnsigned(sbyte value) => unchecked((byte)value);
    }
}
