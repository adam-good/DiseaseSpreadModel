using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiseaseSpreadModel.Models;
using System.Collections.ObjectModel;

namespace DiseaseSpreadModel.ViewModels
{
    public class PopulationViewModel : ViewModelBase
    {
        public static Random Random = new Random();
        public ObservableCollection<PersonModel> Population { get; private set; }
        private List<int> ContactRateDistribution { get; set; }

        private PopulationSettings settings;
        public PopulationSettings Settings
        {
            get { return settings; }
            set { settings = value; RaisePropertyChangedEvent("Settings"); }
        }
        private DiseaseModel disease;

        public PopulationViewModel(PopulationSettings _settings, DiseaseModel _disease)
        {
            Population = new ObservableCollection<PersonModel>();
            settings = _settings;
            disease = _disease;

            ContactRateDistribution = new List<int>();
        }

        public void InitializePopulation()
        {
            MathNet.Numerics.Distributions.Normal contactRateGeneration = new MathNet.Numerics.Distributions.Normal(settings.ContactRateMean, settings.ContactRateStandardDeviation);

            ContactRateDistribution = new List<int>();
            for (int personId = 0; personId < settings.PopulationSize; personId++)
            {
                int contactRate = (int)contactRateGeneration.Sample();
                for (int j = 0; j < contactRate; j++)
                {
                    ContactRateDistribution.Add(personId);
                }

                Enums.InfectionStateEnum initialInfectionState;
                if (personId == 0 )
                {
                    initialInfectionState = Enums.InfectionStateEnum.Infected;
                }
                else
                {
                    initialInfectionState = Enums.InfectionStateEnum.Healthy;
                }

                PersonModel newPersonModel = new PersonModel(personId, contactRate, initialInfectionState, disease);
                Population.Add(newPersonModel);
            }
        }

        public void UpdatePopulation()
        {
            List<PersonModel> updatedPopulation = new List<PersonModel>();
            Parallel.ForEach(Population, (person) =>
            {
                person.Recover();
            });

            Dictionary<int, List<int>> interactionList = new Dictionary<int, List<int>>();

            foreach (var person in Population)
            {
                int index = Random.Next(ContactRateDistribution.Count);

                int personInteractedWithID = ContactRateDistribution[index];
                if(interactionList.ContainsKey(personInteractedWithID))
                {
                    interactionList[personInteractedWithID].Add(person.PersonId);
                }
                else
                {
                    interactionList[personInteractedWithID] = new List<int>() { person.PersonId };
                }

                if(interactionList.ContainsKey(person.PersonId))
                {
                    interactionList[person.PersonId].Add(personInteractedWithID);
                }
                else
                {
                    interactionList[person.PersonId] = new List<int>() { personInteractedWithID };
                }
            }

            Parallel.ForEach(Population, (person) =>
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
            });

            Population.Clear();
            foreach(var person in updatedPopulation)
            {
                Population.Add(person);
            }
        }
    }
}
