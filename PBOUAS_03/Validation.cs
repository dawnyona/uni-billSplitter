using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PBOUAS_03
{
    public class NumericFieldValidation : ValidationRule
    {
        private const string InvalidInput = "Please enter valid number!";

        // Implementing the abstract method in the Validation Rule class
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double val;
            if (!string.IsNullOrEmpty((string)value))
            {
                // Validates weather Non numeric values are entered as the Age
                if (!double.TryParse(value.ToString(), out val))
                {
                    return new ValidationResult(false, InvalidInput);
                }
            }

            return new ValidationResult(true, null);
        }
    }

    public class NullableValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString()))
                return null;
            return value;
        }
    }
}
