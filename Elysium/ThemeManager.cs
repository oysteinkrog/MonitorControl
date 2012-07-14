using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium
{
    [PublicAPI]
    public static class ThemeManager
    {
        [PublicAPI]
        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.RegisterAttached("Theme", typeof(Theme?), typeof(ThemeManager),
                                                new FrameworkPropertyMetadata(null,
                                                                              FrameworkPropertyMetadataOptions.AffectsRender, OnThemeChanged));

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
                var theme = (Theme?)e.NewValue;
                if (theme != null)
                {
                    control.ApplyTheme(theme, null, null);
                }
                else
                {
                    control.RemoveTheme(true, false, false);
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
                var accentBrush = (SolidColorBrush)e.NewValue;
                if (accentBrush != null)
                {
                    control.ApplyTheme(null, accentBrush, null);
                }
                else
                {
                    control.RemoveTheme(false, true, false);
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
                var contrastBrush = (SolidColorBrush)e.NewValue;
                if (contrastBrush != null)
                {
                    control.ApplyTheme(null, null, contrastBrush);
                }
                else
                {
                    control.RemoveTheme(false, false, true);
                }
            }
        }

        private delegate void ApplyThemeToApplicationDelegate(Application application, Theme? theme, SolidColorBrush accentBrush, SolidColorBrush contrastBrush);

        private delegate void RemoveThemeFromApplicationDelegate(
            Application application, bool isRemoveTheme, bool isRemoveAccentBrush, bool isRemoveContrastBrush);

        [PublicAPI]
        [SecuritySafeCritical]
        public static void ApplyTheme(this Application application, Theme? theme, SolidColorBrush accentBrush, SolidColorBrush contrastBrush)
        {
            ValidationHelper.NotNull(application, () => application);

            application.Dispatcher.Invoke(new ApplyThemeToApplicationDelegate(ApplyThemeInternal), DispatcherPriority.Render,
                                          application, theme, accentBrush, contrastBrush);
        }

        [SecurityCritical]
        private static void ApplyThemeInternal(Application application, Theme? theme, SolidColorBrush accentBrush, SolidColorBrush contrastBrush)
        {
            ValidationHelper.NotNull(application, () => application);

            // Resource dictionaries paths
            var lightBrushesUri = new Uri("/Elysium;component/Themes/LightBrushes.xaml", UriKind.Relative);
            var darkBrushesUri = new Uri("/Elysium;component/Themes/DarkBrushes.xaml", UriKind.Relative);

            // Resource dictionaries
            var lightBrushesDictionary = new ResourceDictionary { Source = lightBrushesUri };
            var darkBrushesDictionary = new ResourceDictionary { Source = darkBrushesUri };

            if (theme == Theme.Light)
            {
                // Add LightBrushes.xaml, if not included
                if (application.Resources.MergedDictionaries.All(dictionary => dictionary.Source != lightBrushesUri))
                {
                    application.Resources.MergedDictionaries.Add(lightBrushesDictionary);
                }

                // Remove DarkBrushes.xaml, if included
                var darkColorsDictionaries = application.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == darkBrushesUri).ToList();
                foreach (var dictionary in darkColorsDictionaries)
                {
                    application.Resources.MergedDictionaries.Remove(dictionary);
                }
            }
            if (theme == Theme.Dark)
            {
                // Add DarkBrushes.xaml, if not included
                if (application.Resources.MergedDictionaries.All(dictionary => dictionary.Source != darkBrushesUri))
                {
                    application.Resources.MergedDictionaries.Add(darkBrushesDictionary);
                }

                // Remove LightBrushes.xaml, if included
                var lightColorsDictionaries = application.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == lightBrushesUri).ToList();
                foreach (var dictionary in lightColorsDictionaries)
                {
                    application.Resources.MergedDictionaries.Remove(dictionary);
                }
            }

            // Bug in WPF 4: http://connect.microsoft.com/VisualStudio/feedback/details/555322/global-wpf-styles-are-not-shown-when-using-2-levels-of-references
            if (application.Resources.Keys.Count == 0)
            {
                application.Resources.Add(typeof(Window), new Style(typeof(Window)));
            }

            if (accentBrush != null)
            {
                var accentBrushFrozen = !accentBrush.IsFrozen && accentBrush.CanFreeze ? accentBrush.GetAsFrozen() : accentBrush;
                if (application.Resources.Contains("AccentBrush"))
                {
                    // Set AccentBrush value, if key exist
                    application.Resources["AccentBrush"] = accentBrushFrozen;
                }
                else
                {
                    // Add AccentBrush key and value, if key doesn't exist
                    application.Resources.Add("AccentBrush", accentBrushFrozen);
                }
            }

            if (contrastBrush != null)
            {
                var contrastBrushFrozen = !contrastBrush.IsFrozen && contrastBrush.CanFreeze ? contrastBrush.GetAsFrozen() : contrastBrush;
                if (application.Resources.Contains("ContrastBrush"))
                {
                    // Set ContrastBrush value, if key exist
                    application.Resources["ContrastBrush"] = contrastBrushFrozen;
                }
                else
                {
                    // Add ContrastBrush key and value, if key doesn't exist
                    application.Resources.Add("ContrastBrush", contrastBrushFrozen);
                }

                var semitransparentContrastBrush = contrastBrush.Clone();
                semitransparentContrastBrush.Opacity = 1d / 8d;
                var semitransparentContrastBrushFrozen = !semitransparentContrastBrush.IsFrozen && semitransparentContrastBrush.CanFreeze
                                                             ? semitransparentContrastBrush.GetAsFrozen()
                                                             : semitransparentContrastBrush;
                if (application.Resources.Contains("SemitransparentContrastBrush"))
                {
                    // Set SemitransparentContrastBrush value, if key exist
                    application.Resources["SemitransparentContrastBrush"] = semitransparentContrastBrushFrozen;
                }
                else
                {
                    // Add SemitransparentContrastBrush key and value, if key doesn't exist
                    application.Resources.Add("SemitransparentContrastBrush", semitransparentContrastBrushFrozen);
                }
            }

            // Add Generic.xaml, if not included
            var genericDictionaryUri = new Uri("/Elysium;component/Themes/Generic.xaml", UriKind.Relative);
            if (application.Resources.MergedDictionaries.All(dictionary => dictionary.Source != genericDictionaryUri))
            {
                application.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = genericDictionaryUri });
            }

            OnThemeChanged();
        }

        [PublicAPI]
        [SecuritySafeCritical]
        public static void RemoveTheme(this Application application, bool isRemoveTheme, bool isRemoveAccentBrush, bool isRemoveContrastBrush)
        {
            ValidationHelper.NotNull(application, () => application);

            application.Dispatcher.Invoke(new RemoveThemeFromApplicationDelegate(RemoveThemeInternal), DispatcherPriority.Render,
                                          application, isRemoveTheme, isRemoveAccentBrush, isRemoveContrastBrush);
        }

        [SecurityCritical]
        private static void RemoveThemeInternal(this Application application, bool isRemoveTheme, bool isRemoveAccentBrush, bool isRemoveContrastBrush)
        {
            ValidationHelper.NotNull(application, () => application);

            if (isRemoveTheme)
            {
                // Resource dictionaries paths
                var lightBrushesUri = new Uri("/Elysium;component/Themes/LightBrushes.xaml", UriKind.Relative);
                var darkBrushesUri = new Uri("/Elysium;component/Themes/DarkBrushes.xaml", UriKind.Relative);

                // Remove LightBrushes.xaml, if included
                var lightBrushesDictionaries = application.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == lightBrushesUri).ToList();
                foreach (var dictionary in lightBrushesDictionaries)
                {
                    application.Resources.MergedDictionaries.Remove(dictionary);
                }

                // Remove DarkBrushes.xaml, if included
                var darkBrushesDictionaries = application.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == darkBrushesUri).ToList();
                foreach (var dictionary in darkBrushesDictionaries)
                {
                    application.Resources.MergedDictionaries.Remove(dictionary);
                }

                // Remove Generic.xaml, if included
                var genericDictionaryUri = new Uri("/Elysium;component/Themes/Generic.xaml", UriKind.Relative);
                foreach (var genericDictionary in application.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == genericDictionaryUri))
                {
                    application.Resources.MergedDictionaries.Remove(genericDictionary);
                }
            }

            // Remove AccentBrush resource
            if (isRemoveAccentBrush && application.Resources.Contains("AccentBrush"))
            {
                application.Resources.Remove("AccentBrush");
            }

            // Remove ContrastBrush resource
            if (isRemoveContrastBrush && application.Resources.Contains("ContrastBrush"))
            {
                application.Resources.Remove("ContrastBrush");
            }

            OnThemeChanged();
        }

        private delegate void ApplyThemeToControlDelegate(FrameworkElement control, Theme? theme, SolidColorBrush accentBrush, SolidColorBrush contrastBrush);

        private delegate void RemoveThemeFromControlDelegate(FrameworkElement control, bool isRemoveTheme, bool isRemoveAccentBrush, bool isRemoveContrastBrush);

        [PublicAPI]
        [SecuritySafeCritical]
        private static void ApplyTheme(this FrameworkElement control, Theme? theme, SolidColorBrush accentBrush, SolidColorBrush contrastBrush)
        {
            ValidationHelper.NotNull(control, () => control);

            control.Dispatcher.Invoke(new ApplyThemeToControlDelegate(ApplyThemeInternal), DispatcherPriority.Render,
                                      control, theme, accentBrush, contrastBrush);
        }

        [SecurityCritical]
        private static void ApplyThemeInternal(this FrameworkElement control, Theme? theme, SolidColorBrush accentBrush, SolidColorBrush contrastBrush)
        {
            ValidationHelper.NotNull(control, () => control);

            // Resource dictionaries paths
            var lightBrushesUri = new Uri("/Elysium;component/Themes/LightBrushes.xaml", UriKind.Relative);
            var darkBrushesUri = new Uri("/Elysium;component/Themes/DarkBrushes.xaml", UriKind.Relative);

            // Resource dictionaries
            var lightBrushesDictionary = new ResourceDictionary { Source = lightBrushesUri };
            var darkBrushesDictionary = new ResourceDictionary { Source = darkBrushesUri };

            if (theme == Theme.Light)
            {
                // Add LightBrushes.xaml, if not included
                if (control.Resources.MergedDictionaries.All(dictionary => dictionary.Source != lightBrushesUri))
                {
                    control.Resources.MergedDictionaries.Add(lightBrushesDictionary);
                }

                // Remove DarkBrushes.xaml, if included
                var darkColorsDictionaries = control.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == darkBrushesUri).ToList();
                foreach (var dictionary in darkColorsDictionaries)
                {
                    control.Resources.MergedDictionaries.Remove(dictionary);
                }
            }
            if (theme == Theme.Dark)
            {
                // Add DarkBrushes.xaml, if not included
                if (control.Resources.MergedDictionaries.All(dictionary => dictionary.Source != darkBrushesUri))
                {
                    control.Resources.MergedDictionaries.Add(darkBrushesDictionary);
                }

                // Remove LightBrushes.xaml, if included
                var lightColorsDictionaries = control.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == lightBrushesUri).ToList();
                foreach (var dictionary in lightColorsDictionaries)
                {
                    control.Resources.MergedDictionaries.Remove(dictionary);
                }
            }

            // Bug in WPF 4: http://connect.microsoft.com/VisualStudio/feedback/details/555322/global-wpf-styles-are-not-shown-when-using-2-levels-of-references
            if (control.Resources.Keys.Count == 0)
            {
                control.Resources.Add(typeof(Window), new Style(typeof(Window)));
            }

            if (accentBrush != null)
            {
                var accentBrushFrozen = !accentBrush.IsFrozen && accentBrush.CanFreeze ? accentBrush.GetAsFrozen() : accentBrush;
                if (control.Resources.Contains("AccentBrush"))
                {
                    // Set AccentBrush value, if key exist
                    control.Resources["AccentBrush"] = accentBrushFrozen;
                }
                else
                {
                    // Add AccentBrush key and value, if key doesn't exist
                    control.Resources.Add("AccentBrush", accentBrushFrozen);
                }
            }

            if (contrastBrush != null)
            {
                var contrastBrushFrozen = !contrastBrush.IsFrozen && contrastBrush.CanFreeze ? contrastBrush.GetAsFrozen() : contrastBrush;
                if (control.Resources.Contains("ContrastBrush"))
                {
                    // Set ContrastBrush value, if key exist
                    control.Resources["ContrastBrush"] = contrastBrushFrozen;
                }
                else
                {
                    // Add ContrastBrush key and value, if key doesn't exist
                    control.Resources.Add("ContrastBrush", contrastBrushFrozen);
                }

                var semitransparentContrastBrush = contrastBrush.Clone();
                semitransparentContrastBrush.Opacity = 1d / 8d;
                var semitransparentContrastBrushFrozen = !semitransparentContrastBrush.IsFrozen && semitransparentContrastBrush.CanFreeze
                                                             ? semitransparentContrastBrush.GetAsFrozen()
                                                             : semitransparentContrastBrush;
                if (control.Resources.Contains("SemitransparentContrastBrush"))
                {
                    // Set SemitransparentContrastBrush value, if key exist
                    control.Resources["SemitransparentContrastBrush"] = semitransparentContrastBrushFrozen;
                }
                else
                {
                    // Add SemitransparentContrastBrush key and value, if key doesn't exist
                    control.Resources.Add("SemitransparentContrastBrush", semitransparentContrastBrushFrozen);
                }
            }

            // Add Generic.xaml, if not included
            var genericDictionaryUri = new Uri("/Elysium;component/Themes/Generic.xaml", UriKind.Relative);
            if (control.Resources.MergedDictionaries.All(dictionary => dictionary.Source != genericDictionaryUri))
            {
                control.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = genericDictionaryUri });
            }

            OnThemeChanged();
        }

        [PublicAPI]
        [SecuritySafeCritical]
        private static void RemoveTheme(this FrameworkElement control, bool isRemoveTheme, bool isRemoveAccentBrush, bool isRemoveContrastBrush)
        {
            ValidationHelper.NotNull(control, () => control);

            control.Dispatcher.Invoke(new RemoveThemeFromControlDelegate(RemoveThemeInternal), DispatcherPriority.Render,
                                      control, isRemoveTheme, isRemoveAccentBrush, isRemoveContrastBrush);
        }

        [SecurityCritical]
        private static void RemoveThemeInternal(this FrameworkElement control, bool isRemoveTheme, bool isRemoveAccentBrush, bool isRemoveContrastBrush)
        {
            ValidationHelper.NotNull(control, () => control);

            if (isRemoveTheme)
            {
                // Resource dictionaries paths
                var lightBrushesUri = new Uri("/Elysium;component/Themes/LightBrushes.xaml", UriKind.Relative);
                var darkBrushesUri = new Uri("/Elysium;component/Themes/DarkBrushes.xaml", UriKind.Relative);

                // Remove LightBrushes.xaml, if included
                var lightBrushesDictionaries = control.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == lightBrushesUri).ToList();
                foreach (var dictionary in lightBrushesDictionaries)
                {
                    control.Resources.MergedDictionaries.Remove(dictionary);
                }

                // Remove DarkBrushes.xaml, if included
                var darkBrushesDictionaries = control.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == darkBrushesUri).ToList();
                foreach (var dictionary in darkBrushesDictionaries)
                {
                    control.Resources.MergedDictionaries.Remove(dictionary);
                }

                // Remove Generic.xaml, if included
                var genericDictionaryUri = new Uri("/Elysium;component/Themes/Generic.xaml", UriKind.Relative);
                foreach (var genericDictionary in control.Resources.MergedDictionaries.Where(dictionary => dictionary.Source == genericDictionaryUri).ToList())
                {
                    control.Resources.MergedDictionaries.Remove(genericDictionary);
                }
            }

            // Remove AccentBrush resource
            if (isRemoveAccentBrush && control.Resources.Contains("AccentBrush"))
            {
                control.Resources.Remove("AccentBrush");
            }

            // Remove ContrastBrush resource
            if (isRemoveContrastBrush && control.Resources.Contains("ContrastBrush"))
            {
                control.Resources.Remove("ContrastBrush");
            }

            OnThemeChanged();
        }

        [SecurityCritical]
        private static void OnThemeChanged()
        {
            var systemColors = typeof(SystemColors);
            var invalidateColors = systemColors.GetMethod("InvalidateCache", BindingFlags.Static | BindingFlags.NonPublic);
            if (invalidateColors != null)
            {
                invalidateColors.Invoke(null, null);
            }

            var systemParameters = typeof(SystemParameters);
            var invalidateParameters = systemParameters.GetMethod("InvalidateCache", BindingFlags.Static | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            if (invalidateParameters != null)
            {
                invalidateParameters.Invoke(null, null);
            }

            var presentationFramework = Assembly.GetAssembly(typeof(Window));
            if (presentationFramework != null)
            {
                var systemResources = presentationFramework.GetType("System.Windows.SystemResources");

                if (systemResources != null)
                {
                    var onThemeChanged = systemResources.GetMethod("OnThemeChanged", BindingFlags.Static | BindingFlags.NonPublic);
                    if (onThemeChanged != null)
                    {
                        onThemeChanged.Invoke(null, null);
                    }

                    var invalidateResources = systemResources.GetMethod("InvalidateResources", BindingFlags.Static | BindingFlags.NonPublic);
                    if (invalidateResources != null)
                    {
                        invalidateResources.Invoke(null, new object[] { false });
                    }
                }
            }
        }
    }
} ;