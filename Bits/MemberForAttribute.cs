using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Medallion
{
    /// <summary>
    /// Marker attribute to designate members which require special-casing other than <see cref="long"/>
    /// </summary>
    [Conditional("NEVER_RETAIN")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal sealed class MemberForAttribute : Attribute
    {
        public MemberForAttribute(Type type) { }
    }
}
