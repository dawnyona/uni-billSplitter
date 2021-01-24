using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PBOUAS_03
{
    class TaxCalculatorVM : ObservableObject, ErrorMessage
    {
        // Properties
        public ObservableCollection<TaxCalculator> Calculation { get; set; }

        // Command Handlers
        public CommandHandler submitButton { get; set; }

        // Input Handlers
        private double _income;
        public double Income
        {
            get
            {
                return _income;
            }
            set
            {
                try
                {
                    _income = value;
                    if (_income < 0) { _income = 0; throw new InvalidLogicException(); }
                    OnPropertyChanged(nameof(Income));
                }
                catch(InvalidLogicException x){showMessage(x.Message);}

            }
        }

        private double _tax;
        public double Tax
        {
            get
            {
                return _tax;
            }
            set
            {
                try
                {
                    string temp = value.ToString();
                    if (double.TryParse(temp, out _tax)) { _tax = value; }
                    else { value = 0; }
                    if  (_tax < 0 || _tax > 100) { _tax = 0; throw new InvalidLogicException();}
                  
                }
                catch(FormatException x) { showMessage(x.Message);}
                catch(InvalidLogicException x) { showMessage(x.Message);  }
            }
        }

      
       // Constructor
        public TaxCalculatorVM() 
        {
            Calculation = new ObservableCollection<TaxCalculator>();
            submitButton = new CommandHandler(submit);
        }

        // Methods
        private void submit()
        {
            Calculation.Clear();
            double total = Income + (Income * (Tax / 100));
            Calculation.Add(new TaxCalculator("Income: Rp.", Income));
            Calculation.Add(new TaxCalculator("Tax: %", Tax));
            Calculation.Add(new TaxCalculator("Total: Rp.", total));
        }

        public void showMessage(string message)
        {
            MessageBox.Show($"Tax Calculator : {message}");
        }
    }
}
