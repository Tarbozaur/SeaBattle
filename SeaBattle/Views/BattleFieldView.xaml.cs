﻿using SeaBattle.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeaBattle.Views {
    /// <summary>
    /// Interaction logic for BattleFieldView.xaml
    /// </summary>
    public partial class BattleFieldView : UserControl {
        public BattleFieldView() {
            //DataContext = new BattleFieldViewViewModel();
            InitializeComponent();
        }
    }
}
