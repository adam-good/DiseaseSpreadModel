using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string tempText;
        public string TempText
        {
            get { return tempText; }
            set { tempText = value; RaisePropertyChangedEvent("TempText"); }
        }

        private SimulationViewModel simulationViewModel;
        public SimulationViewModel SimulationViewModel
        {
            get { return simulationViewModel; }
            set { simulationViewModel = value; RaisePropertyChangedEvent("SimulationViewModel"); }
        }

        public MainWindowViewModel()
        {
            SimulationViewModel = new SimulationViewModel();
        }
    }
}
