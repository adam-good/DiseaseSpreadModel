using DiseaseSpreadModel.Enums;
using DiseaseSpreadModel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DiseaseSpreadModel.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        public ObservableCollection<KeyValuePair<int, int>> Infected { get; private set; }
        public ObservableCollection<KeyValuePair<int, int>> Healthy { get; private set; }
        public ObservableCollection<KeyValuePair<int, int>> Recovered { get; private set; }

        private int currentInfected;
        public int CurrentInfected
        {
            get { return currentInfected; }
            set { currentInfected = value; RaisePropertyChangedEvent("CurrentInfected"); }
        }

        private int currentHealthy;
        public int CurrentHealthy
        {
            get { return currentHealthy; }
            set { currentHealthy = value; RaisePropertyChangedEvent("CurrentHealthy"); }
        }

        private int currentRecovered;
        public int CurrentRecovered
        {
            get { return currentRecovered; }
            set { currentRecovered = value; RaisePropertyChangedEvent("CurrentRecovered"); }
        }

        public StatisticsViewModel()
        {
            Infected = new ObservableCollection<KeyValuePair<int, int>>();
            Healthy = new ObservableCollection<KeyValuePair<int, int>>();
            Recovered = new ObservableCollection<KeyValuePair<int, int>>();
        }
        
        public void AddTimestepStatistics(int simulationTime, PopulationViewModel populationViewModel)
        {
            int infectedCount = new List<PersonModel>(populationViewModel.Population).Count(c => c.InfectionState == InfectionStateEnum.Infected);
            Infected.Add(new KeyValuePair<int,int>(simulationTime, infectedCount));
            CurrentInfected = infectedCount;

            int healthyCount = new List<PersonModel>(populationViewModel.Population).Count(c => c.InfectionState == InfectionStateEnum.Healthy);
            Healthy.Add(new KeyValuePair<int, int>(simulationTime, healthyCount));
            CurrentHealthy = healthyCount;

            int recoveredCount = new List<PersonModel>(populationViewModel.Population).Count(c => c.InfectionState == InfectionStateEnum.Recovered);
            Recovered.Add(new KeyValuePair<int, int>(simulationTime, recoveredCount));
            CurrentRecovered = recoveredCount;
        }
    }
}