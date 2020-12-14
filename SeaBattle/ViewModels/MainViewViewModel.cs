using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace SeaBattle.ViewModels {
    internal class MainViewViewModel {
        public ICommand StartButtonCommand { get; }

        private bool CanStartButtonCommandExecute(object p) => true;
        private void OnStartButtonCommandExecuted(object p) {
            SelectShipWindow selectShipWindow = new SelectShipWindow(BattleWindowOpener);
            selectShipWindow.Owner = Application.Current.MainWindow;
            selectShipWindow.Show();
        }
        public ICommand StartBotButtonCommand { get; }

        private bool CanStartBotButtonCommandExecute(object p) => true;
        private void OnStartBotButtonCommandExecuted(object p) {
            SelectShipWindow selectShipWindow = new SelectShipWindow(BattleWindowOpener, true);
            selectShipWindow.Owner = Application.Current.MainWindow;
            selectShipWindow.Show();
        }
        public ICommand HelpButtonCommand { get; }

        private bool CanHelpButtonCommandExecute(object p) => true;
        private void OnHelpButtonCommandExecuted(object p) {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Owner = Application.Current.MainWindow;
            helpWindow.Show();
        }
        public ICommand StatisticsButtonCommand { get; }

        private bool CanStatisticsButtonCommandExecute(object p) => true;
        private void OnStatisticsButtonCommandExecuted(object p) {
            StatisticsWindow statisticsWindow = new StatisticsWindow();
            statisticsWindow.Owner = Application.Current.MainWindow;
            statisticsWindow.Show();
        }
        public MainViewViewModel() {
            StartButtonCommand = new Command(OnStartButtonCommandExecuted, CanStartButtonCommandExecute);
            StartBotButtonCommand = new Command(OnStartBotButtonCommandExecuted, CanStartBotButtonCommandExecute);
            HelpButtonCommand = new Command(OnHelpButtonCommandExecuted, CanHelpButtonCommandExecute);
            StatisticsButtonCommand = new Command(OnStatisticsButtonCommandExecuted, CanStatisticsButtonCommandExecute);
        }
        private void BattleWindowOpener(string firstName, string secondName, List<List<bool>> FirstPlayerMatrix, List<List<bool>> SecondPlayerMatrix, bool hasBot) {
            BattleFieldWindow battleFieldWindow = new BattleFieldWindow(firstName, secondName, FirstPlayerMatrix, SecondPlayerMatrix, hasBot);
            battleFieldWindow.Owner = Application.Current.MainWindow;
            battleFieldWindow.Show();
        }
    }
}
