using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBOUAS_03
{
    public class TaxCalculator
    {
        public string Type { get; set; }
        public double Amount { get; set; }

        private double _income;
        private double _tax;
        public TaxCalculator(string Type, double Amount)
        {
            this.Type = Type;
            this.Amount = Amount;

            if (Type == "Income")
            {
                _income = this.Amount;
            }
            else if (Type == "Tax")
            {
                _tax = this.Amount;
            }
        }

    }

}
