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
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;

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

        private DayOfWeek currentDayOfWeek;
        public DayOfWeek CurrentDayOfWeek
        {
            get { return currentDayOfWeek; }
            set { currentDayOfWeek = value; RaisePropertyChangedEvent("CurrentDayOfWeek"); }
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
        public DelegateCommand ExportCommand { get; private set; }

        public Enums.RunStateEnum RunState { get; set; }

        private DispatcherTimer SimulationTimer;

        public SimulationViewModel()
        {
            PopulationSettings = new PopulationSettings();
            Disease = new DiseaseModel("Default", 0.25f, 0.1f, 3.0f, 5.0f); //TODO: fix this, this is literally aids

            ObservableCollection<DaysOffKeyValuePair> daysOffDefault = new ObservableCollection<DaysOffKeyValuePair>()
            {
                new DaysOffKeyValuePair(DayOfWeek.Sunday, false),
                new DaysOffKeyValuePair(DayOfWeek.Monday, false),
                new DaysOffKeyValuePair(DayOfWeek.Tuesday, false),
                new DaysOffKeyValuePair(DayOfWeek.Wednesday, false),
                new DaysOffKeyValuePair(DayOfWeek.Thursday, false),
                new DaysOffKeyValuePair(DayOfWeek.Friday, false),
                new DaysOffKeyValuePair(DayOfWeek.Saturday, false),

            };

            SimulationSettings = new SimulationSettings(100, daysOffDefault);
            PopulationViewModel = new PopulationViewModel(PopulationSettings, disease);
            PopulationViewModel.InitializePopulation();

            SettingsCommand = new DelegateCommand(Settings);
            RunCommand = new DelegateCommand(Run);
            ResetCommand = new DelegateCommand(Reset);
            PauseCommand = new DelegateCommand(Pause);
            ExportCommand = new DelegateCommand(Export);

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
            bool simulationWasRunning = false;
            if(RunState == Enums.RunStateEnum.Run)
            {
                Pause();
                simulationWasRunning = true;
            }

            SettingsView popup = new SettingsView(new SettingsViewModel(SimulationSettings, Disease, PopulationSettings));
            bool? result = popup.ShowDialog();
            if(result != null && result.Value)
            {
                SettingsViewModel viewModel = (SettingsViewModel)popup.DataContext;
                PopulationSettings = viewModel.PopulationSettings;
                SimulationSettings = viewModel.SimulationSettings;
                Disease = viewModel.DiseaseSettings;

                Reset();
            }
            else if(simulationWasRunning)
            {
                // Unpauses the simulation
                Pause();
            }
        }

        public void Run()
        {
            Reset();
            SimulationTimer.Start();

            RunState = Enums.RunStateEnum.Run;
        }

        private void Simulation_Tick(object sender, EventArgs e)
        {
            PopulationViewModel.UpdatePopulation(SimulationSettings.DaysOffList.FirstOrDefault((d) => d.Key == CurrentDayOfWeek).Value);

            SimulationTime++;
            CurrentDayOfWeek = (DayOfWeek)(SimulationTime % 7);

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
            CurrentDayOfWeek = (DayOfWeek)(SimulationTime % 7);

            PopulationViewModel = new PopulationViewModel(PopulationSettings, Disease);
            PopulationViewModel.InitializePopulation();

            StatisticsViewModel = new StatisticsViewModel();
            StatisticsViewModel.AddTimestepStatistics(SimulationTime, PopulationViewModel);

            SimulationTimer.Interval = new TimeSpan(0, 0, 0, 0, SimulationSettings.CycleSpeed);
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

        public void Export()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "csv";
            bool? result = saveFileDialog.ShowDialog();
            if (result != null && result.Value == true)
            {
                using (TextWriter stream = new StreamWriter(saveFileDialog.FileName))
                {
                    stream.WriteLine("Simulation Time, Healthy, Infected, Recovered");
                    foreach (var key in statisticsViewModel.Keys)
                    {
                        int healthy = statisticsViewModel.Healthy.First(w => w.Key == key).Value;
                        int infected = statisticsViewModel.Infected.First(w => w.Key == key).Value;
                        int recovered = statisticsViewModel.Recovered.First(w => w.Key == key).Value;

                        stream.WriteLine("{0},{1},{2},{3}", key, healthy, infected, recovered);
                    }
                }
            }
        }
    }
}
