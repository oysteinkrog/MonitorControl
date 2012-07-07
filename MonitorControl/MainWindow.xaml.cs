using MonitorControl.Model;

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

            // temporary setup 
            _monitorModel = new MultiMonController();
            DataContext = _viewModel = new ViewModel(_monitorModel);
        }
    }
}