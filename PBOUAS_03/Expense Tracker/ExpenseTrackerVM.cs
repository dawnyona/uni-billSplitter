using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PBOUAS_03
{
    public class ExpenseTrackerVM : ObservableObject, ErrorMessage // Inhertance and Interface
    {
        // Properties
        public List<Expense> Expenses { get; set; }
        public ExpenseOverviewVM overviewVM { get; set; }


        // Command handlers
        public CommandHandler addButton { get; private set; }
        public CommandHandler overviewButton { get; private set; }

        // Input Handlers
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        private string _itemName;
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                _itemName = value;
                OnPropertyChanged(nameof(ItemName));
            }
        }

        private float _price;
        public float Price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private DateTime _date = DateTime.Now.Date;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private string _category;
        public string Category
        {
            get
            {
                return _category;
            }

            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        // Constructor
        public ExpenseTrackerVM()
        {
            Expenses = new List<Expense>();
            overviewVM = new ExpenseOverviewVM();
            addButton = new CommandHandler(add);
            overviewButton = new CommandHandler(overviewVM.openWindow);
        }

        // Methods
        private void add()
        {
            try
            {
              
                if (string.IsNullOrEmpty(ItemName)) { throw new EmptyException();}
                ExpenseTBL tb = new ExpenseTBL();
                tb.ID = ((DateTime.Now.Ticks / 10) % 1000000000).ToString();
                tb.Item = ItemName;
                tb.Price = Price;
                tb.Date = Date;
                if (!string.IsNullOrEmpty(Category))
                {
                    tb.Category = Category;
                }
                else { throw new ArgumentNullException(); }
                if (Price < 1)
                {
                    throw new ZeroPriceException();
                }
                ExpenseOverviewVM.ExpenseDB.ExpenseTBLs.InsertOnSubmit(tb);
                ExpenseOverviewVM.ExpenseDB.SubmitChanges();

                MessageBoxResult message = MessageBox.Show("Your item has been added");
            }
            catch (EmptyException x) { showMessage(x.Message); }
            catch (ArgumentNullException) { showMessage("Please pick a category"); }
            catch (ZeroPriceException x) { showMessage(x.Message); }
        }

        public void showMessage(string message)
        {
            MessageBox.Show($"Expense Tracker : {message}");
        }
    }
}
