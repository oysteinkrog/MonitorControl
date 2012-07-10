using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Elysium;
using Elysium.Notifications;
using Elysium.Parameters;
using MonitorControl.Model;
using MonitorControl.ViewModels;
using Brushes = System.Drawing.Brushes;

namespace MonitorControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly MultiMonController _monitorModel;
        private ViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            SourceInitialized += HandleSourceInitialized;

            // temporary setup 
            _monitorModel = new MultiMonController();
            DataContext = _viewModel = new ViewModel(_monitorModel);
        }

        private void HandleSourceInitialized(object sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private static readonly string Windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        private static readonly string SegoeUI = Windows + @"\Fonts\SegoeUI.ttf";
        private static readonly string Verdana = Windows + @"\Fonts\Verdana.ttf";

        private void ThemeGlyphInitialized(object sender, EventArgs e)
        {
            ThemeGlyph.FontUri = new Uri(File.Exists(SegoeUI) ? SegoeUI : Verdana);
        }

        private void AccentGlyphInitialized(object sender, EventArgs e)
        {
            AccentGlyph.FontUri = new Uri(File.Exists(SegoeUI) ? SegoeUI : Verdana);
        }

        private void ContrastGlyphInitialized(object sender, EventArgs e)
        {
            ContrastGlyph.FontUri = new Uri(File.Exists(SegoeUI) ? SegoeUI : Verdana);
        }

        private void LightClick(object sender, RoutedEventArgs e)
        {
            Application.Current.ApplyTheme(Theme.Light, null, null);
        }

        private void DarkClick(object sender, RoutedEventArgs e)
        {
            Application.Current.ApplyTheme(Theme.Dark, null, null);
        }

        private void AccentClick(object sender, RoutedEventArgs e)
        {
            var item = e.Source as System.Windows.Controls.MenuItem;
            if (item != null)
            {
                var accentBrush = (SolidColorBrush)((System.Windows.Shapes.Rectangle)item.Icon).Fill;
                Application.Current.ApplyTheme(null, accentBrush, null);
            }
        }

        private void WhiteClick(object sender, RoutedEventArgs e)
        {
            Application.Current.ApplyTheme(null, null, System.Windows.Media.Brushes.White);
        }

        private void BlackClick(object sender, RoutedEventArgs e)
        {
            Application.Current.ApplyTheme(null, null, System.Windows.Media.Brushes.Black);
        }

        private void HandleStateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                ShowInTaskbar = false;
            }
            else if (WindowState == WindowState.Normal)
            {
                ShowInTaskbar = true;
            }
        }

        private void HandleTrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }
            else if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Minimized;
            }
        }
    }
}