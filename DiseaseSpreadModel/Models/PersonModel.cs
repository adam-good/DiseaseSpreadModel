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
        public static Random Random = new Random();
        public int PersonId { get; private set; }
        public int ContactRate { get; private set; }
        public InfectionStateEnum InfectionState { get; private set; }
        public DiseaseModel Disease { get; set; }
        
        private int daysInfected;

        public PersonModel(PersonModel person)
        {
            PersonId = person.PersonId;
            ContactRate = person.ContactRate;
            InfectionState = person.InfectionState;
            Disease = person.Disease;

            daysInfected = person.daysInfected;
        }

        public PersonModel(int personId, int contactRate, InfectionStateEnum startingInfectionState, DiseaseModel disease)
        {
            PersonId = personId;
            ContactRate = contactRate;
            InfectionState = startingInfectionState;
            Disease = disease;

            daysInfected = 0;
        }

        public void Update(List<PersonModel> peopleInteractedWith)
        {
            if(InfectionState == InfectionStateEnum.Infected)
            {
                daysInfected++;
            }

            if (InfectionState != InfectionStateEnum.Recovered)
            {
                foreach (PersonModel personInteractedWith in peopleInteractedWith)
                {
                    if (personInteractedWith.InfectionState == InfectionStateEnum.Infected)
                    {
                        if (Random.NextDouble() < Disease.InfectionRate)
                        {
                            InfectionState = InfectionStateEnum.Infected;
                        }
                    }
                }
            }
        }

        public void Recover()
        {
            if(daysInfected >= Disease.AverageInfectionPeriod)
            {
                InfectionState = InfectionStateEnum.Recovered;
            }
        }
    }
}
