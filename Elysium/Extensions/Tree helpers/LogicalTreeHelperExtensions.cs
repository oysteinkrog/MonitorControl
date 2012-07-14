using System.Windows;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [PublicAPI]
    public static class LogicalTreeHelperExtensions
    {
        [PublicAPI]
        public static T FindParent<T>(DependencyObject reference)
            where T : DependencyObject
        {
            var currentParent = LogicalTreeHelper.GetParent(reference);
            var parent = currentParent as T;
            return parent ?? (currentParent != null ? FindParent<T>(currentParent) : null);
        }

        [PublicAPI]
        public static DependencyObject FindTopLevelParent(DependencyObject reference)
        {
            var parent = LogicalTreeHelper.GetParent(reference);
            if (parent != null)
            {
                var nextParent = LogicalTreeHelper.GetParent(parent);
                return nextParent != null ? FindTopLevelParent(parent) : parent;
            }
            return null;
        }
    }
} ;