using System.Collections.ObjectModel;
using Elysium.Theme.ViewModels;
using MonitorControl.Model;

namespace MonitorControl
{
    public class ViewModel : ViewModelBase
    {
        private readonly MultiMonController _monitorModel;

        public ViewModel(MultiMonController monitorModel)
        {
            _monitorModel = monitorModel;

            Monitors = new ObservableCollection<MonitorVM>();
            foreach (var monitor in _monitorModel.Monitors)
            {
                Monitors.Add(new MonitorVM(monitor));
            }
        }


        public ObservableCollection<MonitorVM> Monitors { get; private set; }
    }
}