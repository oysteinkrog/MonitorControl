using System.Diagnostics.Contracts;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [UsedImplicitly]
    internal static class BoxingHelper<T>
        where T : struct
    {
        [UsedImplicitly]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        internal static T Unbox(object value)
        {
            Contract.Assume(value is T);
            return (T)value;
        }
    }
} ;