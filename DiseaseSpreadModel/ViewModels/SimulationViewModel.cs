using DiseaseSpreadModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.ViewModels
{
    class SimulationViewModel : ViewModelBase
    {
        // Simulation Settings

        public DelegateCommand RunCommand { get; private set; }
        public DelegateCommand ResetCommand { get; private set; }
        public DelegateCommand PauseCommand { get; private set; }

        public SimulationViewModel()
        {
            RunCommand = new DelegateCommand(Run);
            ResetCommand = new DelegateCommand(Reset);
            PauseCommand = new DelegateCommand(Pause);
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }
    }
}
