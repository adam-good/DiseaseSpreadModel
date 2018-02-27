using DiseaseSpreadModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.Models
{
    class PersonModel
    {
        public float ContactRate { get; private set; }
        public InfectionStateEnum InfectionState { get; private set; }

        public PersonModel(float contactRate, InfectionStateEnum startingInfectionState)
        {
            ContactRate = contactRate;
            InfectionState = startingInfectionState;
        }
    }
}
