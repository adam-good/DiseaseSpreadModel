using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.Models
{
    class PopulationSettings
    {
        public int PopulationSize { get; private set; }
        public float ContactRateMean { get; private set; }
        public float ContactRateStandardDeviation { get; private set; }
        public float InitialInfectionPercentage { get; private set; }

        public PopulationSettings(int populationSize, float contactRateMean, float contactRateStandardDeviation, float initialInfectionPercentage)
        {
            PopulationSize = populationSize;
            ContactRateMean = contactRateMean;
            ContactRateStandardDeviation = contactRateStandardDeviation;
            InitialInfectionPercentage = initialInfectionPercentage;
        }
    }
}
