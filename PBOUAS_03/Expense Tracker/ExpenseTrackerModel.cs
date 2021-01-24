using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBOUAS_03
{
    public class Expense { 
        public string Category { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }

        public Expense(string ItemName, double Price, DateTime Date, string Category)
        {
            this.Category = Category;
            this.ItemName = ItemName;
            this.Price = Price;
            this.Date = Date.ToString("dd/MM/yyyy");
        }

    }
}
 