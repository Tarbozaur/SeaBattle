using SeaBattle.ViewModels;
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
using System.Windows.Shapes;

namespace SeaBattle {
    /// <summary>
    /// Interaction logic for BattleFieldWindow.xaml
    /// </summary>
    public partial class BattleFieldWindow : Window {

        public BattleFieldWindow(string firstName, string secondName, List<List<bool>> FirstPlayerMatrix, List<List<bool>> SecondPlayerMatrix, bool hasBot) {
            DataContext = new BattleFieldViewViewModel(firstName, secondName, FirstPlayerMatrix, SecondPlayerMatrix, hasBot);
            InitializeComponent();
        }
    }
}
