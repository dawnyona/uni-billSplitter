using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Data;

namespace PBOUAS_03
{


    class HomePageVM : ObservableObject, ErrorMessage
    {
        // Properties
        private double expense;
        private delegate void Load(StreamReader reader);
        private delegate void Save(StreamWriter writer);
        

        // Command Handelrs
        public CommandHandler SaveUser { get; set; }
        public CommandHandler SaveNotes { get; set; }

        // Input Handlers
        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; OnPropertyChanged(nameof(Notes)); }
        }


        private static double _income;
        public double Income
        {
            get
            {
                return _income;
            }
            set
            {
                _income = value;
                OnPropertyChanged(nameof(Income));
                OnPropertyChanged(nameof(NetIncome));
            }
        }

        private string _netIncome;
        public string NetIncome
        {
            get
            {
                if (_income < expense) { return _netIncome = "Your expense exceeds your income :("; }
                else { return _netIncome = (_income - expense).ToString(); }
            }
            set
            {
                _netIncome = value;
                OnPropertyChanged(nameof(NetIncome));
            }
        }

        private string _personName;
        public string PersonName
        {
            get { return _personName; }
            set { _personName = value; OnPropertyChanged(nameof(PersonName)); }
        }


        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }


        private string _bankNumber;
        public string BankNumber
        {
            get { return _bankNumber; }
            set { _bankNumber = value; OnPropertyChanged(nameof(BankNumber)); }
        }


        // Constructor
        public HomePageVM()
        {
            loadData("notes.txt", loadNotes);
            loadData("userDetails.txt", loadDetails);
            SaveUser = new CommandHandler(() => saveData("userDetails.txt", saveDetails));
            SaveNotes = new CommandHandler(() => saveData("notes.txt", saveNotes));
            try
            {
                expense = ExpenseOverviewVM.ExpenseDB.ExpenseTBLs.Sum(item => item.Price);
            }
            catch (InvalidOperationException) { expense = 0; }

        }

        // Methods
        public void showMessage(string message)
        {
            MessageBox.Show($"Home Page : {message}");
        }

        private void loadData(string filename, Load load)
        {
            StreamReader reader = new StreamReader(filename);
            load.Invoke(reader);
            reader.Close();

        }

        private void loadNotes(StreamReader reader)
        {
            string line = reader.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                Notes = line;
            }
        }


        private void loadDetails(StreamReader reader)
        {
            string[] temp = new string[3];
            for (int i = 0; i < 3; i++)
            {
                string line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    temp[i] = line;
                }
            }

            PersonName = temp[0];
            PhoneNumber = temp[1];
            BankNumber = temp[2];
        }


        private void saveData(string filename, Save function)
        {
            StreamWriter writer = new StreamWriter(filename, false);
            function.Invoke(writer);
            writer.Close();
            MessageBox.Show("Saved!", "Notice");
        }

        private void saveNotes(StreamWriter writer)
        {
            writer.WriteLine(Notes);
        }

        private void saveDetails(StreamWriter writer)
        {
            writer.WriteLine(PersonName);
            writer.WriteLine(PhoneNumber);
            writer.WriteLine(BankNumber);
        }
    }
}
