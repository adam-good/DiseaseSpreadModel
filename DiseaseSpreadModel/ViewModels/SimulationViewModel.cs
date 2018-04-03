using DiseaseSpreadModel.Commands;
using DiseaseSpreadModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Diagnostics;
using DiseaseSpreadModel.Views;

namespace DiseaseSpreadModel.ViewModels
{
    public class SimulationViewModel : ViewModelBase
    {
        private PopulationSettings populationSettings;
        public PopulationSettings PopulationSettings
        {
            get { return populationSettings; }
            set { populationSettings = value; RaisePropertyChangedEvent("PopulationSettings"); }
        }
        private SimulationSettings simulationSettings;
        public SimulationSettings SimulationSettings
        {
            get { return simulationSettings; }
            set { simulationSettings = value; RaisePropertyChangedEvent("SimulationSettings"); }
        }

        private DiseaseModel disease;
        public DiseaseModel Disease
        {
            get { return disease; }
            set { disease = value; RaisePropertyChangedEvent("Disease"); }
        }


        private PopulationViewModel populationViewModel;
        public PopulationViewModel PopulationViewModel
        {
            get { return populationViewModel; }
            set { populationViewModel = value;  RaisePropertyChangedEvent("PopulationViewModel"); }
        }

        private int simulationTime;
        public int SimulationTime
        {
            get { return simulationTime; }
            set { simulationTime = value; RaisePropertyChangedEvent("SimulationTime"); }
        }

        private StatisticsViewModel statisticsViewModel;
        public StatisticsViewModel StatisticsViewModel
        {
            get { return statisticsViewModel; }
            set { statisticsViewModel = value;  RaisePropertyChangedEvent("StatisticsViewModel"); }
        }

        public DelegateCommand SettingsCommand { get; private set; }
        public DelegateCommand RunCommand { get; private set; }
        public DelegateCommand ResetCommand { get; private set; }
        public DelegateCommand PauseCommand { get; private set; }

        public Enums.RunStateEnum RunState { get; set; }

        private DispatcherTimer SimulationTimer;

        public SimulationViewModel()
        {
            PopulationSettings = new PopulationSettings();
            Disease = new DiseaseModel("Default", 0.25f, 0.1f, 3.0f, 5.0f); //TODO: fix this, this is literally aids
            SimulationSettings = new SimulationSettings(100);
            PopulationViewModel = new PopulationViewModel(PopulationSettings, disease);
            PopulationViewModel.InitializePopulation();

            SettingsCommand = new DelegateCommand(Settings);
            RunCommand = new DelegateCommand(Run);
            ResetCommand = new DelegateCommand(Reset);
            PauseCommand = new DelegateCommand(Pause);

            SimulationTimer = new DispatcherTimer();
            SimulationTimer.Interval = new TimeSpan(0, 0, 0, 0, SimulationSettings.CycleSpeed);
            SimulationTimer.Tick += new EventHandler(Simulation_Tick);

            SimulationTime = 0;
            RunState = Enums.RunStateEnum.Stop;

            StatisticsViewModel = new StatisticsViewModel();
            StatisticsViewModel.AddTimestepStatistics(SimulationTime, PopulationViewModel);
        }


        public void Settings()
        {
            SettingsView popup = new SettingsView(new SettingsViewModel(SimulationSettings, Disease, PopulationSettings));
            bool? result = popup.ShowDialog();
            if(result != null && result.Value)
            {
                PopulationSettings = ((SettingsViewModel)popup.DataContext).PopulationSettings;
                SimulationSettings = ((SettingsViewModel)popup.DataContext).SimulationSettings;
                Disease = ((SettingsViewModel)popup.DataContext).DiseaseSettings;
            }
        }

        public void Run()
        {
            SimulationTimer.Start();

            SimulationTime = 0;

            PopulationViewModel = new PopulationViewModel(PopulationSettings, disease);
            PopulationViewModel.InitializePopulation();

            StatisticsViewModel = new StatisticsViewModel();
            StatisticsViewModel.AddTimestepStatistics(SimulationTime, PopulationViewModel);

            RunState = Enums.RunStateEnum.Run;
        }

        private void Simulation_Tick(object sender, EventArgs e)
        {
            PopulationViewModel.UpdatePopulation();

            SimulationTime++;

            StatisticsViewModel.AddTimestepStatistics(SimulationTime, PopulationViewModel);
            
            if(StatisticsViewModel.CurrentInfected == 0)
            {
                SimulationTimer.Stop();
                RunState = Enums.RunStateEnum.Stop;
            }
        }

        public void Reset()
        {
            SimulationTimer.Stop();
            RunState = Enums.RunStateEnum.Stop;

            SimulationTime = 0;

            PopulationViewModel = new PopulationViewModel(PopulationSettings, Disease);
            PopulationViewModel.InitializePopulation();

            StatisticsViewModel = new StatisticsViewModel();
            StatisticsViewModel.AddTimestepStatistics(SimulationTime, PopulationViewModel);
        }

        public void Pause()
        {
            if ( RunState == Enums.RunStateEnum.Run)
            {
                SimulationTimer.Stop();
                RunState = Enums.RunStateEnum.Pause;
            }
            else
            {
                SimulationTimer.Start();
                RunState = Enums.RunStateEnum.Run;
            }
        }
    }
}
