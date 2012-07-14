using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class TabItem
    {
        [PublicAPI]
        public static readonly DependencyProperty HeaderStyleProperty =
            DependencyProperty.RegisterAttached("HeaderStyle", typeof(Style), typeof(TabItem),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.TabItem))]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Style GetHeaderStyle([NotNull] System.Windows.Controls.TabItem obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (Style)obj.GetValue(HeaderStyleProperty);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetHeaderStyle([NotNull] System.Windows.Controls.TabItem obj, Style value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(HeaderStyleProperty, value);
        }
    }
} ;