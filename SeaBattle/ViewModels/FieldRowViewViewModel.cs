using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SeaBattle.ViewModels {
    class FieldRowViewViewModel : ViewModelBase {
        public event BtnHandler NotifyButtonColor = (int i, int j) => { };
        public bool isEnabled = true;
        public static int lastRowIndex = -1;
        public static int currentRowIndex = -1;
        public int rowIndex;
        public static int lastBtnClicked = -1;
        public static int currentBtnClicked = -1;
        bool inBattle = false;
        ObservableCollection<bool> _IsButtonEnabled;
        public ObservableCollection<bool> IsButtonEnabled {
            get => _IsButtonEnabled;
            set => Set(ref _IsButtonEnabled, value);
        }
        ObservableCollection<Brush> _Backgrounds;
        public ObservableCollection<Brush> Backgrounds {
            get => _Backgrounds;
            set => Set(ref _Backgrounds, value);
        }
        public ICommand Button1Command { get; }

        private bool CanButton1CommandExecute(object p) => true;
        private void OnButton1CommandExecuted(object p) {
            if (!isEnabled) return;
            lastRowIndex = currentRowIndex;
            currentRowIndex = rowIndex;
            lastBtnClicked = currentBtnClicked;
            currentBtnClicked = 0;
            if (inBattle)
                NotifyButtonColor(currentRowIndex, currentBtnClicked);
        }
        public ICommand Button2Command { get; }

        private bool CanButton2CommandExecute(object p) => true;
        private void OnButton2CommandExecuted(object p) {
            if (!isEnabled) return;
            lastRowIndex = currentRowIndex;
            currentRowIndex = rowIndex;
            lastBtnClicked = currentBtnClicked;
            currentBtnClicked = 1;
            if (inBattle)
                NotifyButtonColor(currentRowIndex, currentBtnClicked);
        }
        public ICommand Button3Command { get; }

        private bool CanButton3CommandExecute(object p) => true;
        private void OnButton3CommandExecuted(object p) {
            if (!isEnabled) return;
            lastRowIndex = currentRowIndex;
            currentRowIndex = rowIndex;
            lastBtnClicked = currentBtnClicked;
            currentBtnClicked = 2;
            if (inBattle)
                NotifyButtonColor(currentRowIndex, currentBtnClicked);
        }
        public ICommand Button4Command { get; }

        private bool CanButton4CommandExecute(object p) => true;
        private void OnButton4CommandExecuted(object p) {
            if (!isEnabled) return;
            lastRowIndex = currentRowIndex;
            currentRowIndex = rowIndex;
            lastBtnClicked = currentBtnClicked;
            currentBtnClicked = 3;
            if (inBattle)
                NotifyButtonColor(currentRowIndex, currentBtnClicked);
        }
        public ICommand Button5Command { get; }

        private bool CanButton5CommandExecute(object p) => true;
        private void OnButton5CommandExecuted(object p) {
            if (!isEnabled) return;
            lastRowIndex = currentRowIndex;
            currentRowIndex = rowIndex;
            lastBtnClicked = currentBtnClicked;
            currentBtnClicked = 4;
            if (inBattle)
                NotifyButtonColor(currentRowIndex, currentBtnClicked);
        }
        public ICommand Button6Command { get; }

        private bool CanButton6CommandExecute(object p) => true;
        private void OnButton6CommandExecuted(object p) {
            if (!isEnabled) return;
            lastRowIndex = currentRowIndex;
            currentRowIndex = rowIndex;
            lastBtnClicked = currentBtnClicked;
            currentBtnClicked = 5;
            if (inBattle)
                NotifyButtonColor(currentRowIndex, currentBtnClicked);
        }
        public ICommand Button7Command { get; }

        private bool CanButton7CommandExecute(object p) => true;
        private void OnButton7CommandExecuted(object p) {
            if (!isEnabled) return;
            lastRowIndex = currentRowIndex;
            currentRowIndex = rowIndex;
            lastBtnClicked = currentBtnClicked;
            currentBtnClicked = 6;
            if (inBattle)
                NotifyButtonColor(currentRowIndex, currentBtnClicked);
        }
        public ICommand Button8Command { get; }

        private bool CanButton8CommandExecute(object p) => true;
        private void OnButton8CommandExecuted(object p) {
            if (!isEnabled) return;
            lastRowIndex = currentRowIndex;
            currentRowIndex = rowIndex;
            lastBtnClicked = currentBtnClicked;
            currentBtnClicked = 7;
            if (inBattle)
                NotifyButtonColor(currentRowIndex, currentBtnClicked);
        }
        public ICommand Button9Command { get; }

        private bool CanButton9CommandExecute(object p) => true;
        private void OnButton9CommandExecuted(object p) {
            if (!isEnabled) return;
            lastRowIndex = currentRowIndex;
            currentRowIndex = rowIndex;
            lastBtnClicked = currentBtnClicked;
            currentBtnClicked = 8;
            if (inBattle)
                NotifyButtonColor(currentRowIndex, currentBtnClicked);
        }
        public ICommand Button10Command { get; }

        private bool CanButton10CommandExecute(object p) => true;
        private void OnButton10CommandExecuted(object p) {
            if (!isEnabled) return;
            lastRowIndex = currentRowIndex;
            currentRowIndex = rowIndex;
            lastBtnClicked = currentBtnClicked;
            currentBtnClicked = 9;
            if (inBattle)
                NotifyButtonColor(currentRowIndex, currentBtnClicked);
        }
        public FieldRowViewViewModel(int rowIndex, bool inBattle = false) {
            this.inBattle = inBattle;
            Button1Command = new Command(OnButton1CommandExecuted, CanButton1CommandExecute);
            Button2Command = new Command(OnButton2CommandExecuted, CanButton2CommandExecute);
            Button3Command = new Command(OnButton3CommandExecuted, CanButton3CommandExecute);
            Button4Command = new Command(OnButton4CommandExecuted, CanButton4CommandExecute);
            Button5Command = new Command(OnButton5CommandExecuted, CanButton5CommandExecute);
            Button6Command = new Command(OnButton6CommandExecuted, CanButton6CommandExecute);
            Button7Command = new Command(OnButton7CommandExecuted, CanButton7CommandExecute);
            Button8Command = new Command(OnButton8CommandExecuted, CanButton8CommandExecute);
            Button9Command = new Command(OnButton9CommandExecuted, CanButton9CommandExecute);
            Button10Command = new Command(OnButton10CommandExecuted, CanButton10CommandExecute);
            
            this.rowIndex = rowIndex;
            IsButtonEnabled = new ObservableCollection<bool>();
            for (int i = 0; i < 10; ++i) {
                IsButtonEnabled.Add(true);
            }
            Backgrounds = new ObservableCollection<Brush>();
            for (int i = 0; i < 10; ++i) {
                Backgrounds.Add(Brushes.White);
            }
        }

    }
}
