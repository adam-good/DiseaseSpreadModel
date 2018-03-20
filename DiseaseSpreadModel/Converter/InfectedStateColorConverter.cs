using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using DiseaseSpreadModel.Enums;
using System.Windows.Media;
using System.Globalization;

namespace DiseaseSpreadModel.Converter
{
    public class InfectedStateColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((InfectionStateEnum)value == InfectionStateEnum.Healthy)
            {
                return new SolidColorBrush(Colors.LightGreen);
            }
            else if ((InfectionStateEnum)value == InfectionStateEnum.Infected)
            {
                return new SolidColorBrush(Colors.Pink);
            }
            else if ((InfectionStateEnum)value == InfectionStateEnum.Recovered)
            {
                return new SolidColorBrush(Colors.LightSteelBlue);
            }

            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
