using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Elysium;

namespace MonitorControl
{
    using MonitorControl.Model;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }


        private void StartupHandler(object sender, System.Windows.StartupEventArgs e)
        {
            this.ApplyTheme(Theme.Dark, AccentBrushes.Blue, Brushes.White);
        }
    }
}
