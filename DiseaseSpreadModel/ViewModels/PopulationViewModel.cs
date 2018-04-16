using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiseaseSpreadModel.Models;
using System.Collections.ObjectModel;
using DiseaseSpreadModel.Enums;

namespace DiseaseSpreadModel.ViewModels
{
    public class PopulationViewModel : ViewModelBase
    {
        public static Random Random = new Random();
        public ObservableCollection<PopulationModel> PopulationModels { get; private set; }

        private PopulationSettings settings;
        public PopulationSettings Settings
        {
            get { return settings; }
            set { settings = value; RaisePropertyChangedEvent("Settings"); }
        }
        private DiseaseModel disease;
        private SimulationSettings simulationSettings;

        public PopulationViewModel(PopulationSettings _settings, SimulationSettings _simulationSettings, DiseaseModel _disease)
        {
            PopulationModels = new ObservableCollection<PopulationModel>();
            settings = _settings;
            simulationSettings = _simulationSettings;
            disease = _disease;
        }

        public void InitializePopulation()
        {
            MathNet.Numerics.Distributions.Normal contactRateGeneration = new MathNet.Numerics.Distributions.Normal(settings.MeanContactsPerDay, settings.StdDevContactsPerDay);
            MathNet.Numerics.Distributions.Normal contactRateDayOffGeneration = new MathNet.Numerics.Distributions.Normal(settings.MeanContactsPerDayOff, settings.StdDevContactsPerDayOff);

            for (int populationId = 0; populationId < simulationSettings.NumOfRuns; populationId++)
            {
                PopulationModel newModel = new PopulationModel(populationId);
                newModel.InitializePopulation(settings.PopulationSize, contactRateGeneration, contactRateDayOffGeneration, disease);
                PopulationModels.Add(newModel);
            }
        }

        public void UpdatePopulation(bool isDayOff)
        {
            List<PopulationModel> updatedPopulations = new List<PopulationModel>();
            foreach(var population in PopulationModels)
            {
                population.UpdatePopulation(isDayOff, Random);
                updatedPopulations.Add(population);
            };

            PopulationModels.Clear();
            foreach (var updatedPopulation in updatedPopulations)
            {
                PopulationModels.Add(updatedPopulation);
            }
        }

        public double GetAverageInfected()
        {
            int populationsCount = PopulationModels.Count;
            double totalInfected = 0;
            foreach (var populationModel in PopulationModels)
            {
                int infectedCount = populationModel.Population.Count(c => c.InfectionState == InfectionStateEnum.Infected);
                totalInfected += infectedCount;
            }

            return totalInfected / populationsCount;
        }
        public double GetAverageHealthy()
        {
            int populationsCount = PopulationModels.Count;
            double totalHealthy = 0;
            foreach (var populationModel in PopulationModels)
            {
                int healthyCount = populationModel.Population.Count(c => c.InfectionState == InfectionStateEnum.Healthy);
                totalHealthy += healthyCount;
            }

            return totalHealthy / populationsCount;
        }
        public double GetAverageRecovered()
        {
            int populationsCount = PopulationModels.Count;
            double totalRecovered = 0;
            foreach (var populationModel in PopulationModels)
            {
                int recoveredCount = populationModel.Population.Count(c => c.InfectionState == InfectionStateEnum.Recovered);
                totalRecovered += recoveredCount;
            }

            return (double)totalRecovered / populationsCount;
        }
    }
}
