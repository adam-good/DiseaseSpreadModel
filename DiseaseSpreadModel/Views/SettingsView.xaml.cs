using DiseaseSpreadModel.Commands;
using DiseaseSpreadModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiseaseSpreadModel.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsView : Window
    {

        public SettingsView(SettingsViewModel settingsViewModel)
        {
            InitializeComponent();
            this.DataContext = settingsViewModel;
            settingsViewModel.OnSave += SettingsViewModel_OnSave;
        }

        private void SettingsViewModel_OnSave(object sender, EventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }


    }
}
