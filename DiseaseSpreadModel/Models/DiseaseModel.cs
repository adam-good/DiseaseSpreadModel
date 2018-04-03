using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.Models
{
    public class DiseaseModel
    {
        public string DiseaseName { get; set; }
        public float InfectionRate { get; set; }
        public float RecoveryRate { get; set; }
        public float AverageIncubationPeriod { get; set; }
        public float AverageInfectionPeriod { get; set; }

        public DiseaseModel(string diseaseName, float infectionRate, float recoveryRate, float averageIncubationPeriod, float averageInfectionPeriod)
        {
            DiseaseName = diseaseName;
            InfectionRate = infectionRate;
            RecoveryRate = recoveryRate;
            AverageIncubationPeriod = averageIncubationPeriod;
            AverageInfectionPeriod = averageInfectionPeriod;
        }

        public DiseaseModel(DiseaseModel diseaseModel)
        {
            DiseaseName = diseaseModel.DiseaseName;
            InfectionRate = diseaseModel.InfectionRate;
            RecoveryRate = diseaseModel.RecoveryRate;
            AverageIncubationPeriod = diseaseModel.AverageIncubationPeriod;
            AverageInfectionPeriod = diseaseModel.AverageInfectionPeriod;
        }
    }
}
