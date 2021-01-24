using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PBOUAS_03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        // Properties
        public static SettingsWin SettingsWin;
        public static Frame Frame{ get; set; }
        
        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            Frame = new Frame();
            Frame.Content = new homePage();
            DataContext = this;
        }

        // Methods (Directly linked to xaml file)
        private void Selected_Item(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Content.ToString())
            {
                case "Bill Splitter": Frame.Content = new billSplitter(); break;
                case "Expense Tracker": Frame.Content = new expenseTracker(); break;
                case "Tax Calculator": Frame.Content = new taxCalculator(); break;
                case "Settings": SettingsWin = new SettingsWin(); SettingsWin.ShowDialog();  break;
                default: Frame.Content = new homePage(); break;
            }
        }
    }
}
