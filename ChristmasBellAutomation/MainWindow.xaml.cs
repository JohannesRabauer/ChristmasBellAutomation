﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ChristmasBellAutomation
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainContext _context;

        public MainWindow()
        {
            InitializeComponent();
            Closing += onClosing;
            this._context = new MainContext(Properties.Settings.Default.CountdownTimeInSeconds);
            this.DataContext = this._context;
        }
        
        private void onClosing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.CountdownTimeInSeconds = this._context.CountdownTimeInSeconds;
            Properties.Settings.Default.Save();
        }
    }
}
