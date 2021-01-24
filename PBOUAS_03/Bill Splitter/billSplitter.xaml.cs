﻿using System;
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
using System.Collections.ObjectModel;

namespace PBOUAS_03
{
    /// <summary>
    /// Interaction logic for billSplitter.xaml
    /// </summary>
    public partial class billSplitter : Page
    {
        public static BillSplitterVM vm { get; set; }
        public billSplitter()
        {
            InitializeComponent();
            vm = new BillSplitterVM();
            DataContext = vm;
        }
    }
}