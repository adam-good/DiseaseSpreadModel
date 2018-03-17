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
        public ObservableCollection<PersonModel> Population { get; private set; }

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
        }

        public void InitializePopulation()
        {
            Random randomGenerator = new Random();
            MathNet.Numerics.Distributions.Normal contactRateGeneration = new MathNet.Numerics.Distributions.Normal(settings.ContactRateMean, settings.ContactRateStandardDeviation);

            for (int i = 0; i < settings.PopulationSize; i++)
            {
                float contactRate = (float)contactRateGeneration.Sample();
                Enums.InfectionStateEnum initialInfectionState;

                if (i < settings.InitialInfectionAmount)
                {
                    initialInfectionState = Enums.InfectionStateEnum.Infected;
                }
                else
                {
                    initialInfectionState = Enums.InfectionStateEnum.Healthy;
                }

                PersonModel newPersonModel = new PersonModel(i, contactRate, initialInfectionState, disease);
                Population.Add(newPersonModel);
            }
        }

        public void UpdatePopulation()
        {
            Parallel.ForEach(Population, (person) =>
             {
                 person.Update();
             });
        }
    }
}
