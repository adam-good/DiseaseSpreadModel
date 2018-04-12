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
        public float InfectionPeriod { get; set; }

        public DiseaseModel(string diseaseName, float infectionRate, float infectionPeriod)
        {
            DiseaseName = diseaseName;
            InfectionRate = infectionRate;
            InfectionPeriod = infectionPeriod;
        }

        public DiseaseModel(DiseaseModel diseaseModel)
        {
            DiseaseName = diseaseModel.DiseaseName;
            InfectionRate = diseaseModel.InfectionRate;
            InfectionPeriod = diseaseModel.InfectionPeriod;
        }
    }
}
