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

        public MainWindowViewModel()
        {
            TempText = "THIS IS BOUND TO VIEW MODEL";

            var testingSimulationViewModel = new SimulationViewModel();

        }
    }
}
