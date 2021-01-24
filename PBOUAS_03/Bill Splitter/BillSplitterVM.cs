using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.Specialized;

namespace PBOUAS_03
{
    public class BillSplitterVM : ObservableObject
    {
        // Properties
        public static ObservableCollection<Person> GridCollection { get; set; } // Data grid in bill splitter window

        // Link to other VMs
        public PopUpVM popvm { get; set; }
        public CalculateVM calvm { get; set; }


        // Command Handlers
        public CommandHandler popCommand { get; private set; }
        public CommandHandler calculateCommand { get; private set; }
        public CommandHandler deleteCommand { get; private set;}




      
        // Input Handlers
        private Person _selected;
        public Person SelectedUser
        {
            get { return _selected; }
            set { _selected = value; OnPropertyChanged(nameof(SelectedUser));}
       
        }

        // Constructor
        public BillSplitterVM()
        {
            popvm = new PopUpVM();
            GridCollection = new ObservableCollection<Person>();
            GridCollection.CollectionChanged += OnCollectionChanged;
       

            popCommand = new CommandHandler(popvm.PopUp.OpenWindow);
            deleteCommand = new CommandHandler(delete);

           
            calvm = new CalculateVM();
            calculateCommand = new CommandHandler(open);
 
        }


        // Methods
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            calvm.Subtotal = GridCollection.Sum(person => person.Product.Price);
        }

        private void open()
        {
            calvm.ResultCollection = new ObservableCollection<Split>();
            Calculate.LoadData(calvm.ResultCollection, calvm.Discount, calvm.OtherFees);
            calvm.Calculate.OpenWindow();
        }

        private void delete()
        {
            GridCollection.Remove(SelectedUser);
        }
    }
}
