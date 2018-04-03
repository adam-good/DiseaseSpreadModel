using DiseaseSpreadModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.Models
{
    public class DaysOffKeyValuePair
    {
        public WeekDays Key { get; set; }
        public bool Value { get; set; }

        public DaysOffKeyValuePair(WeekDays key, bool value)
        {
            Key = key;
            Value = value;
        }
    }
}
