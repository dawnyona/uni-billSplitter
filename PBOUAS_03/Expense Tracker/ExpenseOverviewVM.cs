using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Linq;
using System.Data.SqlClient;
using System.IO;

namespace PBOUAS_03
{
    public class ExpenseOverviewVM : ObservableObject
    {
        // Properties
        private static OverviewWin _win;
        static string temp = @"D:\PBOUAS_03\PBOUAS_03\ExpenseDatabase.mdf";
        public static SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\ExpenseDatabase.mdf;Integrated Security=True");

        // Database and Datagrid
        public static ExpenseLINQDataContext ExpenseDB = new ExpenseLINQDataContext(connection);
        public ObservableCollection<ExpenseTBL> OverviewGrid { get; set; }
    

        // Command Handlers
        public CommandHandler okButton { get; private set; }
        public CommandHandler deleteButton { get; private set; }

        // Input Handlers
        private double _total;
        public double Total
        {
            get { return _total; }
            set { _total = value; OnPropertyChanged(nameof(Total)); }
        }

        private ExpenseTBL _selectedItem;
        public ExpenseTBL SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }


        // Constructor
        public ExpenseOverviewVM()
        {
            okButton = new CommandHandler(close);
            deleteButton = new CommandHandler(deleteRow);

        }

        // Methods
        private void deleteRow()
        {
            ExpenseDB.ExpenseTBLs.DeleteOnSubmit(SelectedItem);
            ExpenseDB.SubmitChanges();
            OverviewGrid.Remove(SelectedItem);
            Total = (double)ExpenseDB.ExpenseTBLs.Sum(item => item.Price);
        }
         
        private void close()
        {
            _win.Close();
        }

        public void openWindow()
        {
            
            OverviewGrid = new ObservableCollection<ExpenseTBL>(ExpenseDB.ExpenseTBLs.ToList());
       
            try
            {
                Total = (double)ExpenseDB.ExpenseTBLs.Sum(item => item.Price);
            }
            catch (InvalidOperationException) { Total = 0; }
            _win = new OverviewWin();
            _win.ShowDialog();
        }
    }
}
