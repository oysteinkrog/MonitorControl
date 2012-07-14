using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class TabControl
    {
        [PublicAPI]
        public static readonly DependencyProperty IndicatorBrushProperty =
            DependencyProperty.RegisterAttached("IndicatorBrush", typeof(SolidColorBrush), typeof(TabControl),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.TabControl))]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.TabItem))]
        public static SolidColorBrush GetIndicatorBrush([NotNull] DependencyObject obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            ValidationHelper.OfTypes(obj, () => obj, typeof(System.Windows.Controls.TabControl), typeof(System.Windows.Controls.TabItem));
            return (SolidColorBrush)obj.GetValue(IndicatorBrushProperty);
        }

        [PublicAPI]
        public static void SetIndicatorBrush([NotNull] DependencyObject obj, SolidColorBrush value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            ValidationHelper.OfTypes(obj, () => obj, typeof(System.Windows.Controls.TabControl), typeof(System.Windows.Controls.TabItem));
            obj.SetValue(IndicatorBrushProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty IndicatorThicknessProperty =
            DependencyProperty.RegisterAttached("IndicatorThickness", typeof(double), typeof(TabControl),
                                                new FrameworkPropertyMetadata(2d, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.TabControl))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetIndicatorThickness([NotNull] System.Windows.Controls.TabControl obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(IndicatorThicknessProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetIndicatorThickness([NotNull] System.Windows.Controls.TabControl obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(IndicatorThicknessProperty, value);
        }
    }
} ;