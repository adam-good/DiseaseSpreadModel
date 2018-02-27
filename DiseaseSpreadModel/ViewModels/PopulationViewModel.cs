using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiseaseSpreadModel.Models;
using System.Collections.ObjectModel;

namespace DiseaseSpreadModel.ViewModels
{
    class PopulationViewModel : ViewModelBase
    {
        public ObservableCollection<PersonModel> Population { get; private set; }

        private PopulationSettings settings;

        public PopulationViewModel(PopulationSettings _settings)
        {
            Population = new ObservableCollection<PersonModel>();
            settings = _settings;
        }

        public void InitializePopulation()
        {
            Random randomGenerator = new Random();
            MathNet.Numerics.Distributions.Normal contactRateGeneration = new MathNet.Numerics.Distributions.Normal(settings.ContactRateMean, settings.ContactRateStandardDeviation);

            for (int i = 0; i < settings.PopulationSize; i++)
            {
                float contactRate = (float)contactRateGeneration.Sample();
                Enums.InfectionStateEnum initialStartingState;

                double chanceOfInfection = randomGenerator.NextDouble();
                if (chanceOfInfection < settings.InitialInfectionPercentage)
                {
                    initialStartingState = Enums.InfectionStateEnum.Infected;
                }
                else
                {
                    initialStartingState = Enums.InfectionStateEnum.Healthy;
                }

                PersonModel newPersonModel = new PersonModel(contactRate, initialStartingState);
                Population.Add(newPersonModel);
            }
        }

        public void UpdatePopulation()
        {
            throw new NotImplementedException();
        }
    }
}
