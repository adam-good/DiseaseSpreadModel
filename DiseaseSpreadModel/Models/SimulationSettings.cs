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
        public int PopulationSize { get; private set; }
        public float ContactRateMean { get; private set; }
        public float ContactRateStandardDeviation { get; private set; }
        public float InitialInfectionPercentage { get; private set; }

        public SimulationSettings(int maximumStepCount, int populationSize, float contactRateMean, float contactRateStandardDeviation, float initialInfectionPercentage)
        {
            MaximumStepCount = maximumStepCount;
            PopulationSize = populationSize;
            ContactRateMean = contactRateMean;
            ContactRateStandardDeviation = contactRateStandardDeviation;
            InitialInfectionPercentage = initialInfectionPercentage;
        }
    }
}
