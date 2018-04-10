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
        private List<int> ContactRateDayOffDistribution { get; set; }

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
            ContactRateDayOffDistribution = new List<int>();
        }

        public void InitializePopulation()
        {
            MathNet.Numerics.Distributions.Normal contactRateGeneration = new MathNet.Numerics.Distributions.Normal(settings.MeanContactsPerDay, settings.StdDevContactsPerDay);
            MathNet.Numerics.Distributions.Normal contactRateDayOffGeneration = new MathNet.Numerics.Distributions.Normal(settings.MeanContactsPerDayOff, settings.StdDevContactsPerDay);

            ContactRateDistribution = new List<int>();
            for (int personId = 0; personId < settings.PopulationSize; personId++)
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
                if (personId == 0 )
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

        public void UpdatePopulation(bool isDayOff)
        {
            List<PersonModel> updatedPopulation = new List<PersonModel>();
            Parallel.ForEach(Population, (person) =>
            {
                person.Recover();
            });

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
                        int index = Random.Next(ContactRateDistribution.Count);

                        int personInteractedWithID = ContactRateDistribution[index];
                        interactionList[personInteractedWithID].Add(person.PersonId);
                        interactionList[person.PersonId].Add(personInteractedWithID);
                    }                    
                }
            }
            else
            {
                foreach(var person in Population)
                {
                    for(int i = 0; i < Math.Ceiling((double)person.MeanContactsPerDayOff / 2); i++)
                    {
                        int index = Random.Next(ContactRateDayOffDistribution.Count);

                        int personInteractedWithID = ContactRateDayOffDistribution[index];
                        interactionList[personInteractedWithID].Add(person.PersonId);
                        interactionList[person.PersonId].Add(personInteractedWithID);
                    }
                }
            }

            Dictionary<int, int> foo = new Dictionary<int, int>();
            foreach(var f in interactionList)
            {
                if(foo.ContainsKey(f.Value.Count))
                {
                    foo[f.Value.Count]++;
                }
                else
                {
                    foo[f.Value.Count] = 1;
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
