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
        public int NumOfRuns { get; set; }
        public int CycleSpeed { get; set; }

        public ObservableCollection<DaysOffKeyValuePair> DaysOffList { get; set; }

        //TODO: add appropriate properties, framerate, etc..
        public SimulationSettings(int numOfRuns, int cycleSpeed, ObservableCollection<DaysOffKeyValuePair> daysOffList)
        {
            NumOfRuns = numOfRuns;
            CycleSpeed = cycleSpeed;
            DaysOffList = daysOffList;
        }

        public SimulationSettings(SimulationSettings simulationSettings)
        {
            NumOfRuns = simulationSettings.NumOfRuns;
            CycleSpeed = simulationSettings.CycleSpeed;
            DaysOffList = simulationSettings.DaysOffList;
        }
    }
}
