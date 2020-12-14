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
    /// Interaction logic for SelectShipWindow.xaml
    /// </summary>
    public partial class SelectShipWindow : Window {
        public SelectShipWindow(BattleWindowHandler BattleWindowOpener, bool hasBot = false) {
            DataContext = new SelectShipViewViewModel(hasBot);
            ((SelectShipViewViewModel)DataContext).NotifyBattleWindow += BattleWindowOpener;
            InitializeComponent();
        }
    }
}
