using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.Models
{
    class SimulationSettings
    {
        public int MaximumStepCount { get; private set; }
        //TODO: add appropriate properties, framerate, etc..
        public SimulationSettings(int maximumStepCount)
        {
            MaximumStepCount = maximumStepCount;
        }
    }
}
