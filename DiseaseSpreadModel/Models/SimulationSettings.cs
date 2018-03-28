using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.Models
{
    public class SimulationSettings
    {
        public int CycleSpeed { get; set; }
        //TODO: add appropriate properties, framerate, etc..
        public SimulationSettings(int cycleSpeed)
        {
            CycleSpeed = cycleSpeed;
        }
    }
}
