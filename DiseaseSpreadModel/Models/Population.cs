using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.Models
{
    public class PopulationModel
    {
        public List<int> ContactRateDistribution { get; private set; }
        public List<int> ContactRateDayOffDistribution { get; private set; }
        public List<PersonModel> Population { get; private set; }
        public int PopulationId;

        public PopulationModel(int populationId)
        {
            PopulationId = populationId;
            ContactRateDayOffDistribution = new List<int>();
            ContactRateDistribution = new List<int>();
            Population = new List<PersonModel>();
        }

        public void InitializePopulation(int populationSize, MathNet.Numerics.Distributions.Normal contactRateGeneration, MathNet.Numerics.Distributions.Normal contactRateDayOffGeneration, DiseaseModel disease)
        {
            ContactRateDayOffDistribution = new List<int>();
            ContactRateDistribution = new List<int>();
            Population = new List<PersonModel>();
            for (int personId = 0; personId < populationSize; personId++)
            {
                int contactRate = (int)Math.Round(contactRateGeneration.Sample(), 0);
                for (int j = 0; j < contactRate; j++)
                {
                    ContactRateDistribution.Add(personId);
                }

                int contactRateDayOff = (int)Math.Round(contactRateDayOffGeneration.Sample(), 0);
                for (int j = 0; j < contactRateDayOff; j++)
                {
                    ContactRateDayOffDistribution.Add(personId);
                }

                Enums.InfectionStateEnum initialInfectionState;
                if (personId == 0)
                {
                    initialInfectionState = Enums.InfectionStateEnum.Infected;
                }
                else
                {
                    initialInfectionState = Enums.InfectionStateEnum.Healthy;
                }

                PersonModel newPersonModel = new PersonModel(personId, contactRate, contactRateDayOff, initialInfectionState, disease);
                Population.Add(newPersonModel);
            }
        }

        public void UpdatePopulation(bool isDayOff, Random random)
        {

            List<PersonModel> updatedPopulation = new List<PersonModel>();
            foreach(var person in Population)
            {
                person.Recover();
            };

            Dictionary<int, List<int>> interactionList = new Dictionary<int, List<int>>();

            foreach (var person in Population)
            {
                interactionList[person.PersonId] = new List<int>();
            }

            if (!isDayOff)
            {
                foreach (var person in Population)
                {
                    for (int i = 0; i < Math.Ceiling((double)person.MeanContactsPerDay / 2); i++)
                    {
                        int index = random.Next(ContactRateDistribution.Count);

                        int personInteractedWithID = ContactRateDistribution[index];
                        interactionList[personInteractedWithID].Add(person.PersonId);
                        interactionList[person.PersonId].Add(personInteractedWithID);
                    }
                }
            }
            else
            {
                foreach (var person in Population)
                {
                    for (int i = 0; i < Math.Ceiling((double)person.MeanContactsPerDayOff / 2); i++)
                    {
                        int index = random.Next(ContactRateDayOffDistribution.Count);

                        int personInteractedWithID = ContactRateDayOffDistribution[index];
                        interactionList[personInteractedWithID].Add(person.PersonId);
                        interactionList[person.PersonId].Add(personInteractedWithID);
                    }
                }
            }

            foreach(var person in Population)
            {
                var copyPerson = new PersonModel(person);

                List<PersonModel> interactedWith = interactionList[person.PersonId]
                                                        .Select(s => Population.First(f => f.PersonId == s))
                                                        .ToList();

                copyPerson.Update(interactedWith);
                lock (updatedPopulation)
                {
                    updatedPopulation.Add(copyPerson);
                }
            }

            Population.Clear();
            Population.AddRange(updatedPopulation);
        }
    }
}
