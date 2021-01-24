using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PBOUAS_03
{
    public class NumericFieldValidation : ValidationRule // Home page validation
    {
    
        private const string InvalidInput = "Please enter valid number!";
      
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double val;
            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Please enter a number");
            }
            else if (!double.TryParse(value.ToString(), out val))
            {
                return new ValidationResult(false, InvalidInput);
            }
            else { return new ValidationResult(true, value); }
        }
    }


    public class EmptyCellValidation: ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, null);
            }
            else { return new ValidationResult(true, value); }
        }
    }

    public class NullableValueConverter : IValueConverter // Applicable in XAML
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double potentialNum;
            if (string.IsNullOrEmpty(value.ToString())) { return 0; }
            if (double.TryParse(value.ToString(), out potentialNum))
            {
                return value;
            }
            else {return 0; }

        }
    }
   
}
