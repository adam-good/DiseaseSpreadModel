using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.Models
{
    public class SimulationSettings
    {
        public int CycleSpeed { get; set; }

        public ObservableCollection<DaysOffKeyValuePair> DaysOffList { get; set; }

        //TODO: add appropriate properties, framerate, etc..
        public SimulationSettings(int cycleSpeed, ObservableCollection<DaysOffKeyValuePair> daysOffList)
        {
            CycleSpeed = cycleSpeed;
            DaysOffList = daysOffList;
        }

        public SimulationSettings(SimulationSettings simulationSettings)
        {
            CycleSpeed = simulationSettings.CycleSpeed;
            DaysOffList = simulationSettings.DaysOffList;
        }
    }
}
