using System.Windows;
using System.Windows.Media;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [PublicAPI]
    public static class VisualTreeHelperExtensions
    {
        [PublicAPI]
        public static T FindParent<T>(DependencyObject reference)
            where T : DependencyObject
        {
            var currentParent = VisualTreeHelper.GetParent(reference);
            var parent = currentParent as T;
            return parent ?? (currentParent != null ? FindParent<T>(currentParent) : null);
        }

        [PublicAPI]
        public static DependencyObject FindTopLevelParent(DependencyObject reference)
        {
            var parent = VisualTreeHelper.GetParent(reference);
            if (parent != null)
            {
                var nextParent = VisualTreeHelper.GetParent(parent);
                return nextParent != null ? FindTopLevelParent(parent) : parent;
            }
            return null;
        }
    }
} ;