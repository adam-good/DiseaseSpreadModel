using DiseaseSpreadModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.Models
{
   public class PersonModel
    {
        public float ContactRate { get; private set; }
        public InfectionStateEnum InfectionState { get; private set; }
        public DiseaseModel Disease { get; set; }

        public PersonModel(float contactRate, InfectionStateEnum startingInfectionState, DiseaseModel disease)
        {
            ContactRate = contactRate;
            InfectionState = startingInfectionState;
            Disease = disease;
        }

        public void Update()
        {

        }
    }
}
