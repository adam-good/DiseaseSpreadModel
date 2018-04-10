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
        public float MeanContactsPerDay { get; set; }
        public float StdDevContactsPerDay { get; set; }
        public float MeanContactsPerDayOff { get; set; }
        public float StdDevContactsPerDayOff { get; set; }

        public PopulationSettings()
        {
            PopulationSize = 100;
            MeanContactsPerDay = 10;
            StdDevContactsPerDay = 0.01f;

            MeanContactsPerDayOff = 3;
            StdDevContactsPerDayOff = 0.3f;
        }

        public PopulationSettings(int populationSize, float meanContactsPerDay, float stdDevContactsPerDay, float meanContactsPerDayOff, float stdDevContactsPerDayOff)
        {
            PopulationSize = populationSize;
            MeanContactsPerDay = meanContactsPerDay;
            StdDevContactsPerDay = stdDevContactsPerDay;
            MeanContactsPerDay = meanContactsPerDayOff;
            StdDevContactsPerDay = stdDevContactsPerDayOff;
        }

        public PopulationSettings(PopulationSettings populationSettings)
        {
            PopulationSize = populationSettings.PopulationSize;
            MeanContactsPerDay = populationSettings.MeanContactsPerDay;
            StdDevContactsPerDay = populationSettings.StdDevContactsPerDay;
            MeanContactsPerDayOff = populationSettings.MeanContactsPerDayOff;
            StdDevContactsPerDayOff = populationSettings.StdDevContactsPerDayOff;
        }
    }
}
