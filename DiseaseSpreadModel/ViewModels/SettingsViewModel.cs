using DiseaseSpreadModel.Commands;
using DiseaseSpreadModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.ViewModels
{
    public class SettingsViewModel
    {
        public DelegateCommand SaveCommand { get; private set; }

        public event EventHandler OnSave;

        public SimulationSettings SimulationSettings { get; private set; }
        public DiseaseModel DiseaseSettings { get; private set; }
        public PopulationSettings PopulationSettings { get; private set; }

        public SettingsViewModel(SimulationSettings simulationSettings, DiseaseModel disease, PopulationSettings populationSettings)
        {
            SaveCommand = new DelegateCommand(Save);

            PopulationSettings = populationSettings;
            DiseaseSettings = disease;
            SimulationSettings = simulationSettings;
        }
        public void Save()
        {
            EventHandler handler = this.OnSave;
            if(handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

    }
}
