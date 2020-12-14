using SeaBattle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SeaBattle.ViewModels {
    class BattleFieldViewViewModel : ViewModelBase, ICloseWindow {
        bool hasBot;
        bool turnOfFirst = true;
        public Action Close { get; set; }
        FieldViewViewModel _FieldViewViewModelContext1;
        public FieldViewViewModel FieldViewViewModelContext1 {
            get => _FieldViewViewModelContext1;
            set => Set(ref _FieldViewViewModelContext1, value);
        }
        FieldViewViewModel _FieldViewViewModelContext2;
        public FieldViewViewModel FieldViewViewModelContext2 {
            get => _FieldViewViewModelContext2;
            set => Set(ref _FieldViewViewModelContext2, value);
        }
        
        string _FirstName;
        public string FirstName {
            get => _FirstName;
            set => Set(ref _FirstName, value);
        }
        string _SecondName;
        public string SecondName {
            get => _SecondName;
            set => Set(ref _SecondName, value);
        }
        bool isNowFirstPlayerTurn = true;
        Visibility _FirstFieldLeftToRight = Visibility.Visible;
        public Visibility FirstFieldLeftToRight {
            get => _FirstFieldLeftToRight;
            set => Set(ref _FirstFieldLeftToRight, value);
        }
        Visibility _SecondFieldRightToLeft = Visibility.Collapsed;
        public Visibility SecondFieldRightToLeft {
            get => _SecondFieldRightToLeft;
            set => Set(ref _SecondFieldRightToLeft, value);
        }
        public BattleFieldViewViewModel(string firstName, string secondName, List<List<bool>> firstPlayer, List<List<bool>> secondPlayer, bool hasBot) {
            this.hasBot = hasBot;
            FirstName = firstName;
            SecondName = secondName;
            FieldViewViewModelContext1 = new FieldViewViewModel(firstPlayer, 1);
            FieldViewViewModelContext1.NotifyPlayerTurn += HandlePlayerTurn;
            FieldViewViewModelContext1.NotifyWinPlayer += HandleWinPlayer;
            FieldViewViewModelContext2 = new FieldViewViewModel(secondPlayer, 2);
            FieldViewViewModelContext2.NotifyPlayerTurn += HandlePlayerTurn;
            FieldViewViewModelContext2.NotifyWinPlayer += HandleWinPlayer;

            FieldViewViewModelContext1.IsEnabled = false;
            FieldViewViewModelContext2.IsEnabled = true;
        }
        private void HandlePlayerTurn() {
            if (hasBot && turnOfFirst) {
                turnOfFirst = false;
                FieldViewViewModelContext1.ThrowRandomShot();
            } else if (hasBot && !turnOfFirst) {
                turnOfFirst = true;
            } else if(!hasBot) {
                isNowFirstPlayerTurn = !isNowFirstPlayerTurn;
                FieldViewViewModelContext1.IsEnabled = !FieldViewViewModelContext1.IsEnabled;
                FieldViewViewModelContext2.IsEnabled = !FieldViewViewModelContext2.IsEnabled;
                if (FirstFieldLeftToRight == Visibility.Visible) {
                    FirstFieldLeftToRight = Visibility.Collapsed;
                    SecondFieldRightToLeft = Visibility.Visible;
                } else {
                    FirstFieldLeftToRight = Visibility.Visible;
                    SecondFieldRightToLeft = Visibility.Collapsed;
                }
            }
        }
        private void HandleWinPlayer(int i) {
            if (i == 2) {
                if (MessageBox.Show($"{FirstName} Won!") == MessageBoxResult.OK) {
                    using (MyAppContext dBContext = new MyAppContext()) {
                        int max = 0;
                        if(dBContext.History.Count() != 0) 
                            max = dBContext.History.Select(x => x.Id).Max();
                        dBContext.History.AddAsync(
                            new HistoryItem {
                                FirstPlayerName = FirstName,
                                SecondPlayerName = SecondName,
                                WinnerName = FirstName,
                                DateTime = DateTime.Now
                            });
                        dBContext.SaveChangesAsync();
                    }
                    Close();
                }
            } else if (i == 1) {
                if (MessageBox.Show($"{SecondName} Won!") == MessageBoxResult.OK) {
                    using (MyAppContext dBContext = new MyAppContext()) {
                        int max = 0;
                        if (dBContext.History.Count() != 0)
                            max = dBContext.History.Select(x => x.Id).Max();
                        dBContext.History.Add(
                            new HistoryItem {
                                FirstPlayerName = FirstName,
                                SecondPlayerName = SecondName,
                                WinnerName = SecondName,
                                DateTime = DateTime.Now
                            });
                        dBContext.SaveChanges();
                    }
                    Close();
                }
            }
        }
    }
}
