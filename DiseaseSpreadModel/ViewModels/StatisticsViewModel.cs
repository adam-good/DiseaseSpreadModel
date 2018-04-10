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
        public ObservableCollection<KeyValuePair<int, double>> Infected { get; private set; }
        public ObservableCollection<KeyValuePair<int, double>> Healthy { get; private set; }
        public ObservableCollection<KeyValuePair<int, double>> Recovered { get; private set; }

        private double currentInfected;
        public double CurrentInfected
        {
            get { return currentInfected; }
            set { currentInfected = value; RaisePropertyChangedEvent("CurrentInfected"); }
        }

        private double currentHealthy;
        public double CurrentHealthy
        {
            get { return currentHealthy; }
            set { currentHealthy = value; RaisePropertyChangedEvent("CurrentHealthy"); }
        }

        private double currentRecovered;
        public double CurrentRecovered
        {
            get { return currentRecovered; }
            set { currentRecovered = value; RaisePropertyChangedEvent("CurrentRecovered"); }
        }

        public List<int> Keys { get; private set; }

        public StatisticsViewModel()
        {
            Infected = new ObservableCollection<KeyValuePair<int, double>>();
            Healthy = new ObservableCollection<KeyValuePair<int, double>>();
            Recovered = new ObservableCollection<KeyValuePair<int, double>>();

            Keys = new List<int>();
        }
        
        public void AddTimestepStatistics(int simulationTime, PopulationViewModel populationViewModel)
        {
            double infectedCount = populationViewModel.GetAverageInfected();
            Infected.Add(new KeyValuePair<int,double>(simulationTime, infectedCount));
            CurrentInfected = infectedCount;

            double healthyCount = populationViewModel.GetAverageHealthy();
            Healthy.Add(new KeyValuePair<int, double>(simulationTime, healthyCount));
            CurrentHealthy = healthyCount;

            double recoveredCount = populationViewModel.GetAverageRecovered();
            Recovered.Add(new KeyValuePair<int, double>(simulationTime, recoveredCount));
            CurrentRecovered = recoveredCount;

            Keys.Add(simulationTime);
        }
    }
}