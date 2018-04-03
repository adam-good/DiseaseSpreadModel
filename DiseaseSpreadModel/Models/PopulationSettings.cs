using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.Models
{
    public class PopulationSettings
    {
        public int PopulationSize { get; set; }
        public float ContactRateMean { get; set; }
        public float ContactRateStandardDeviation { get; set; }

        public PopulationSettings()
        {
            PopulationSize = 100;
            ContactRateMean = 10;
            ContactRateStandardDeviation = 1.0f;
        }

        public PopulationSettings(int populationSize, float contactRateMean, float contactRateStandardDeviation)
        {
            PopulationSize = populationSize;
            ContactRateMean = contactRateMean;
            ContactRateStandardDeviation = contactRateStandardDeviation;
        }

        public PopulationSettings(PopulationSettings populationSettings)
        {
            PopulationSize = populationSettings.PopulationSize;
            ContactRateMean = populationSettings.ContactRateMean;
            ContactRateStandardDeviation = ContactRateStandardDeviation;
        }
    }
}
