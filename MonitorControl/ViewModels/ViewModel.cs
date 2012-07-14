using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Expression.Interactivity.Core;
using MonitorControl.Model;

namespace MonitorControl.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private readonly MultiMonController _monitorModel;

        public ViewModel()
            : this(new MultiMonController())
        {
        }

        public ViewModel(MultiMonController monitorModel)
        {
            _monitorModel = monitorModel ?? new MultiMonController();

            Monitors = new ObservableCollection<MonitorVM>(_monitorModel.Monitors.Select(a=>new MonitorVM(a)));
        }

        public ObservableCollection<MonitorVM> Monitors { get; private set; }


    }
}