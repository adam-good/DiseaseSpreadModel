using DiseaseSpreadModel.Commands;
using DiseaseSpreadModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Diagnostics;

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

        public DelegateCommand RunCommand { get; private set; }
        public DelegateCommand ResetCommand { get; private set; }
        public DelegateCommand PauseCommand { get; private set; }

        public Enums.RunStateEnum RunState { get; set; }

        private DispatcherTimer SimulationTimer;
        private Stopwatch stopWatch;

        public SimulationViewModel()
        {
            PopulationSettings = new PopulationSettings(1000, 5.0f, 1.0f, 0.04f);
            Disease = new DiseaseModel("AIDS", 0.25f, 0.1f, 3.0f, 5.0f); //TODO: fix this, this is literally aids
            
            PopulationViewModel = new PopulationViewModel(PopulationSettings, disease);
            PopulationViewModel.InitializePopulation();

            RunCommand = new DelegateCommand(Run);
            ResetCommand = new DelegateCommand(Reset);
            PauseCommand = new DelegateCommand(Pause);

            SimulationTimer = new DispatcherTimer();
            SimulationTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            SimulationTimer.Tick += new EventHandler(Simulation_Tick);

            stopWatch = new Stopwatch();
            SimulationTime = 0;
            RunState = Enums.RunStateEnum.Stop;

            StatisticsViewModel = new StatisticsViewModel();
            StatisticsViewModel.AddTimestepStatistics(SimulationTime, PopulationViewModel);
        }

        public void Run()
        {
            SimulationTimer.Start();
            RunState = Enums.RunStateEnum.Run;
        }

        private void Simulation_Tick(object sender, EventArgs e)
        {
            stopWatch.Start();
            PopulationViewModel.UpdatePopulation();
            stopWatch.Stop();

            SimulationTime++;
            stopWatch.Reset();

            StatisticsViewModel.AddTimestepStatistics(SimulationTime, PopulationViewModel);
        }

        public void Reset()
        {
            SimulationTimer.Stop();
            RunState = Enums.RunStateEnum.Stop;
            //TODO: Prompt User to change Simulation and Population Settings

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
