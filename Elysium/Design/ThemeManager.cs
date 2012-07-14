using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Design
{
    [PublicAPI]
    public static class ThemeManager
    {
        [PublicAPI]
        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.RegisterAttached("Theme", typeof(Theme?), typeof(ThemeManager),
                                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnThemeChanged));

        [PublicAPI]
        [SuppressMessage("Microsoft.Contracts", "Nonnull-72-0")]
        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static Theme? GetTheme([NotNull] FrameworkElement obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (Theme?)obj.GetValue(ThemeProperty);
        }

        [PublicAPI]
        public static void SetTheme([NotNull] FrameworkElement obj, Theme? value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ThemeProperty, value);
        }

        [SuppressMessage("Microsoft.Contracts", "Nonnull-17-0")]
        private static void OnThemeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as FrameworkElement;
            if (control != null)
            {
                if (DesignerProperties.GetIsInDesignMode(control))
                {
                    Elysium.ThemeManager.SetTheme(control, (Theme?)e.NewValue);
                }
            }
        }

        [PublicAPI]
        public static readonly DependencyProperty AccentBrushProperty =
            DependencyProperty.RegisterAttached("AccentBrush", typeof(SolidColorBrush), typeof(ThemeManager),
                                                new FrameworkPropertyMetadata(null,
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                                                                              OnAccentBrushChanged));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetAccentBrush([NotNull] FrameworkElement obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (SolidColorBrush)obj.GetValue(AccentBrushProperty);
        }

        [PublicAPI]
        public static void SetAccentBrush([NotNull] FrameworkElement obj, SolidColorBrush value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(AccentBrushProperty, value);
        }

        private static void OnAccentBrushChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as FrameworkElement;
            if (control != null)
            {
                if (DesignerProperties.GetIsInDesignMode(control))
                {
                    Elysium.ThemeManager.SetAccentBrush(control, (SolidColorBrush)e.NewValue);
                }
            }
        }

        [PublicAPI]
        public static readonly DependencyProperty ContrastBrushProperty =
            DependencyProperty.RegisterAttached("ContrastBrush", typeof(SolidColorBrush), typeof(ThemeManager),
                                                new FrameworkPropertyMetadata(null,
                                                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                                                              FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                                                                              OnContrastBrushChanged));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static SolidColorBrush GetContrastBrush([NotNull] FrameworkElement obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return (SolidColorBrush)obj.GetValue(ContrastBrushProperty);
        }

        [PublicAPI]
        public static void SetContrastBrush([NotNull] FrameworkElement obj, SolidColorBrush value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ContrastBrushProperty, value);
        }

        private static void OnContrastBrushChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as FrameworkElement;
            if (control != null)
            {
                if (DesignerProperties.GetIsInDesignMode(control))
                {
                    Elysium.ThemeManager.SetContrastBrush(control, (SolidColorBrush)e.NewValue);
                }
            }
        }
    }
} ;