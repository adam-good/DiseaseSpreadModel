using DiseaseSpreadModel.Commands;
using DiseaseSpreadModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DelegateCommand RunCommand { get; private set; }
        public DelegateCommand ResetCommand { get; private set; }
        public DelegateCommand PauseCommand { get; private set; }

        public Enums.RunStateEnum RunState { get; set; }

        public SimulationViewModel()
        {
            PopulationSettings = new PopulationSettings(100, 5.0f, 1.0f, 0.04f);
            Disease = new DiseaseModel("AIDS", 0.999f, 0.1f, 3.0f, 3.0f); //TODO: fix this, this is literally aids
            
            PopulationViewModel = new PopulationViewModel(PopulationSettings, disease);
            PopulationViewModel.InitializePopulation();

            RunCommand = new DelegateCommand(Run);
            ResetCommand = new DelegateCommand(Reset);
            PauseCommand = new DelegateCommand(Pause);
        }

        public void Run()
        {
            while (RunState != Enums.RunStateEnum.Stop)
            {
                if (RunState != Enums.RunStateEnum.Pause)
                {
                    PopulationViewModel.UpdatePopulation();
                    //TODO: may need to update graphics or something idk.
                }
            }
        }

        public void Reset()
        {
            RunState = Enums.RunStateEnum.Stop;
            //TODO: Prompt User to change Simulation and Population Settings

            PopulationViewModel = new PopulationViewModel(PopulationSettings, Disease);
            PopulationViewModel.InitializePopulation();

        }

        public void Pause()
        {
            if ( RunState == Enums.RunStateEnum.Run)
            {
                RunState = Enums.RunStateEnum.Pause;
            }
            else
            {
                RunState = Enums.RunStateEnum.Run;
            }
        }
    }
}
