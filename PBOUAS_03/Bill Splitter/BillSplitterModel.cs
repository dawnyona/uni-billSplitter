using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace PBOUAS_03
{
    public class Product
    {
        public string Item { get; set; }
        public double Price { get; set; }
     
    }

    public class Person
    {
        public string Name { get; set; }
        public Product Product { get; set; } // Aggregation 

        public Person(Product Product, string Name = " ")
        {
            this.Name = Name;
            this.Product = Product;
        }
    }

    public class Split
    {
        public string Name { get; set; }
        public double ItemsPrice { get; set; }

        public Split(string Name, double ItemsPrice)
        {
            this.Name = Name;
            this.ItemsPrice = ItemsPrice;
        }
    }

    public abstract class Win // Polymorphism, abstract class
    {
        public abstract void OpenWindow();
        public abstract void CloseWindow();
    }

    public class PopUp : Win
    {
        private static billPopUp _win { get; set; }
        public override void OpenWindow()
        {
            _win = new billPopUp();
            _win.ShowDialog();
        }

        public override void CloseWindow()
        {
            _win.Close();
        }

    }

    public class Calculate : Win
    {

        private static calculateWin _win { get; set; }
        public override void OpenWindow()
        {
            _win = new calculateWin();
            _win.ShowDialog();
        }

        public override void CloseWindow()
        {
            _win.Close();
        }
        public static void LoadData(ObservableCollection<Split> Collection, double Discount, double Fees)
        {
            List<Person> grid = BillSplitterVM.GridCollection.ToList();
            IEnumerable<Split> groupingQuery = grid.GroupBy(data => new { data.Name }).Select(g => new Split(g.Key.Name, g.Sum(x => x.Product.Price)));
            int numOfPeople = groupingQuery.Count();
            Fees = Fees / numOfPeople;
            foreach (Split item in groupingQuery)
            {
                item.ItemsPrice = item.ItemsPrice - (item.ItemsPrice * (Discount / 100)) + Fees;
                Collection.Add(item);
            }

        }
    }
}


