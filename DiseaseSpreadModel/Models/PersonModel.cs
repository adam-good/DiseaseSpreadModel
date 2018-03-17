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
        public int PersonId { get; private set; }
        public float ContactRate { get; private set; }
        public InfectionStateEnum InfectionState { get; private set; }
        public DiseaseModel Disease { get; set; }

        public PersonModel(int personId, float contactRate, InfectionStateEnum startingInfectionState, DiseaseModel disease)
        {
            PersonId = personId;
            ContactRate = contactRate;
            InfectionState = startingInfectionState;
            Disease = disease;
        }

        public void Update()
        {

        }
    }
}
