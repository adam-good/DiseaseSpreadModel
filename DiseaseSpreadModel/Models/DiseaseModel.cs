using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.Models
{
    public class DiseaseModel
    {
        public string DiseaseName { get; private set; }
        public float InfectionRate { get; private set; }
        public float RecoveryRate { get; private set; }
        public float AverageIncubationPeriod { get; private set; }
        public float AverageInfectionPeriod { get; private set; }

        public DiseaseModel(string diseaseName, float infectionRate, float recoveryRate, float averageIncubationPeriod, float averageInfectionPeriod)
        {
            DiseaseName = diseaseName;
            InfectionRate = infectionRate;
            RecoveryRate = recoveryRate;
            AverageIncubationPeriod = averageIncubationPeriod;
            AverageInfectionPeriod = averageInfectionPeriod;
        }
    }
}
