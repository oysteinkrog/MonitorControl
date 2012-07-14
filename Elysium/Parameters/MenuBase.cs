using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class MenuBase
    {
        [PublicAPI]
        public static readonly DependencyProperty SubmenuBackgroundProperty =
            DependencyProperty.RegisterAttached("SubmenuBackground", typeof(Brush), typeof(MenuBase),
                                                new FrameworkPropertyMetadata(null,
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [PublicAPI]
        [Pure]
        [System.Diagnostics.Contracts.Pure]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.Primitives.MenuBase))]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Brush GetSubmenuBackground(System.Windows.Controls.Primitives.MenuBase obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (Brush)obj.GetValue(SubmenuBackgroundProperty);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetSubmenuBackground(System.Windows.Controls.Primitives.MenuBase obj, Brush value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(SubmenuBackgroundProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SubmenuBorderBrushProperty =
            DependencyProperty.RegisterAttached("SubmenuBorderBrush", typeof(Brush), typeof(MenuBase),
                                                new FrameworkPropertyMetadata(null,
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [PublicAPI]
        [Pure]
        [System.Diagnostics.Contracts.Pure]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.Primitives.MenuBase))]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Brush GetSubmenuBorderBrush(System.Windows.Controls.Primitives.MenuBase obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (Brush)obj.GetValue(SubmenuBorderBrushProperty);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetSubmenuBorderBrush(System.Windows.Controls.Primitives.MenuBase obj, Brush value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(SubmenuBorderBrushProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty SubmenuBorderThicknessProperty =
            DependencyProperty.RegisterAttached("SubmenuBorderThickness", typeof(Thickness), typeof(MenuBase),
                                                new FrameworkPropertyMetadata(new Thickness(0d),
                                                                              FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                              FrameworkPropertyMetadataOptions.AffectsRender,
                                                                              null, ThicknessUtil.CoerceNonNegative));

        [PublicAPI]
        [Pure]
        [System.Diagnostics.Contracts.Pure]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.Primitives.MenuBase))]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Thickness GetSubmenuBorderThickness(System.Windows.Controls.Primitives.MenuBase obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            ThicknessUtil.EnsureNonNegative();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(SubmenuBorderThicknessProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetSubmenuBorderThickness(System.Windows.Controls.Primitives.MenuBase obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(SubmenuBorderThicknessProperty, value);
        }
    }
} ;