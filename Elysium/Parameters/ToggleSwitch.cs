using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class ToggleSwitch
    {
        [PublicAPI]
        public static readonly DependencyProperty TrackSizeProperty =
            DependencyProperty.RegisterAttached("TrackSize", typeof(double), typeof(ToggleSwitch),
                                                new FrameworkPropertyMetadata(48d, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Controls.ToggleSwitch))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetTrackSize([NotNull] Controls.ToggleSwitch obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(TrackSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetTrackSize([NotNull] Controls.ToggleSwitch obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(TrackSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ThumbThicknessProperty =
            DependencyProperty.RegisterAttached("ThumbThickness", typeof(double), typeof(ToggleSwitch),
                                                new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Controls.ToggleSwitch))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetThumbThickness([NotNull] Controls.ToggleSwitch obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(ThumbThicknessProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetThumbThickness([NotNull] Controls.ToggleSwitch obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ThumbThicknessProperty, value);
        }
    }
} ;