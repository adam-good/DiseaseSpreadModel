using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseSpreadModel.Models
{
    public class DaysOffKeyValuePair
    {
        public DayOfWeek Key { get; set; }
        public bool Value { get; set; }

        public DaysOffKeyValuePair(DayOfWeek key, bool value)
        {
            Key = key;
            Value = value;
        }
    }
}
