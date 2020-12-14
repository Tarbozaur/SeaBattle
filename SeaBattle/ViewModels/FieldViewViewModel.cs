using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SeaBattle.ViewModels {
    public delegate void BtnHandler(int i, int j);
    public delegate void PlayerTurnHandler();
    public delegate void PlayerWinHandler(int i);
    class FieldViewViewModel : ViewModelBase {
        public event PlayerTurnHandler NotifyPlayerTurn = () => { };
        public event PlayerWinHandler NotifyWinPlayer = (int i) => { };
        bool _IsEnabled = true;
        public bool IsEnabled {
            get => _IsEnabled;
            set {
                Set(ref _IsEnabled, value);
                for (int i = 0; i < 10; ++i) {
                    FieldRowViewViewModels[i].isEnabled = value;
                }
            }
        }
        int size = 10;
        int gridNumber;
        int FieldShipsLength1 = 4;
        int FieldShipsLength2 = 3;
        int FieldShipsLength3 = 2;
        int FieldShipsLength4 = 1;
        List<List<bool>> _IsShipMatrix;
        public List<List<bool>> IsShipMatrix {
            get => _IsShipMatrix;
            set => Set(ref _IsShipMatrix, value);
        }
        List<List<int>> _ShotMatrix;
        public List<List<int>> ShotMatrix {
            get => _ShotMatrix;
            set => Set(ref _ShotMatrix, value);
        }
        ObservableCollection<FieldRowViewViewModel> _FieldRowViewViewModels;
        public ObservableCollection<FieldRowViewViewModel> FieldRowViewViewModels {
            get => _FieldRowViewViewModels;
            set => Set(ref _FieldRowViewViewModels, value);
        }
        public FieldViewViewModel() {

            IsShipMatrix = new List<List<bool>>();
            for (int i = 0; i < size; ++i) {
                IsShipMatrix.Add(new List<bool>());
                for (int j = 0; j < size; ++j) {
                    IsShipMatrix[i].Add(false);
                }
            }
            FieldRowViewViewModels = new ObservableCollection<FieldRowViewViewModel>();
            for (int i = 0; i < size; ++i) {
                FieldRowViewViewModels.Add(new FieldRowViewViewModel(i));
            }
        }
        public FieldViewViewModel(List<List<bool>> PlayerMatrix, int gridNumber) {
            this.gridNumber = gridNumber;
            IsShipMatrix = new List<List<bool>>();
            for (int i = 0; i < size; ++i) {
                IsShipMatrix.Add(new List<bool>());
                for (int j = 0; j < size; ++j) {
                    IsShipMatrix[i].Add(PlayerMatrix[i][j]);
                }
            }
            ShotMatrix = new List<List<int>>();
            for (int i = 0; i < 10; ++i) {
                ShotMatrix.Add(new List<int>());
                for (int j = 0; j < 10; ++j) {
                    ShotMatrix[i].Add(0);
                }
            }
            FieldRowViewViewModels = new ObservableCollection<FieldRowViewViewModel>();
            for (int i = 0; i < size; ++i) {
                FieldRowViewViewModel temp = new FieldRowViewViewModel(i, true);
                temp.NotifyButtonColor += ButtonColorHandler;
                FieldRowViewViewModels.Add(temp);
            }
        }
        public void ThrowRandomShot() {
            Random random = new Random();
            int a;
            int b;
            do {
                do {
                    a = random.Next(0, 10);
                    b = random.Next(0, 10);
                } while (ShotMatrix[a][b] != 0);
                ButtonColorHandler(a, b);
            } while (IsShipMatrix[a][b]);
            
        }
        private void ButtonColorHandler(int i, int j) {
            if (ShotMatrix[i][j] == 0 && IsShipMatrix[i][j] == true) {
                FieldRowViewViewModels[i].Backgrounds[j] = Brushes.Orange;
                ShotMatrix[i][j] = 1;
                int count1 = 1;
                int count2 = 1;
                int offset = 1;
                bool nextLeft = true;
                bool nextRight = true;
                bool nextTop = true;
                bool nextBottom = true;
                while (offset != 4) {
                    if (i + offset < 10 && IsShipMatrix[i + offset][j] == true && nextTop) {
                        count2++;
                        if (ShotMatrix[i + offset][j] == 1)
                            count1++;
                    } else {
                        nextTop = false;
                    }
                    if (i - offset >= 0 && IsShipMatrix[i - offset][j] == true && nextBottom) {
                        count2++;
                        if (ShotMatrix[i - offset][j] == 1)
                            count1++;
                    } else {
                        nextBottom = false;
                    }
                    if (j + offset < 10 && IsShipMatrix[i][j + offset] == true && nextRight) {
                        count2++;
                        if (ShotMatrix[i][j + offset] == 1)
                            count1++;
                    } else {
                        nextRight = false;
                    }
                    if (j - offset >= 0 && IsShipMatrix[i][j - offset] == true && nextLeft) {
                        count2++;
                        if (ShotMatrix[i][j - offset] == 1)
                            count1++;
                    } else {
                        nextLeft = false;
                    }
                    offset++;
                }
                offset = 1;
                nextLeft = true;
                nextRight = true;
                nextTop = true;
                nextBottom = true;
                if (count1 == count2) {
                    if (count1 == 1) {
                        FieldShipsLength1--;
                    } else if (count1 == 2) {
                        FieldShipsLength2--;
                    } else if (count1 == 3) {
                        FieldShipsLength3--;
                    } else if (count1 == 4) {
                        FieldShipsLength4--;
                    }
                    FieldRowViewViewModels[i].Backgrounds[j] = Brushes.Red;
                    while (offset != 4) {
                        if (i + offset < 10 && IsShipMatrix[i + offset][j] == true && nextTop) {
                            FieldRowViewViewModels[i + offset].Backgrounds[j] = Brushes.Red;
                        } else {
                            nextTop = false;
                        }
                        if (i - offset >= 0 && IsShipMatrix[i - offset][j] == true && nextBottom) {
                            FieldRowViewViewModels[i - offset].Backgrounds[j] = Brushes.Red;
                        } else {
                            nextBottom = false;
                        }
                        if (j + offset < 10 && IsShipMatrix[i][j + offset] == true && nextRight) {
                            FieldRowViewViewModels[i].Backgrounds[j + offset] = Brushes.Red;
                        } else {
                            nextRight = false;
                        }      
                        if (j - offset >= 0 && IsShipMatrix[i][j - offset] == true && nextLeft) {
                            FieldRowViewViewModels[i].Backgrounds[j - offset] = Brushes.Red;
                        } else {
                            nextLeft = false;
                        }
                        offset++;
                    }
                    if (FieldShipsLength1 == 0 && FieldShipsLength2 == 0
                        && FieldShipsLength3 == 0 && FieldShipsLength4 == 0) {
                        NotifyWinPlayer(gridNumber);
                    }
                }

            } else if (ShotMatrix[i][j] == 0 && IsShipMatrix[i][j] == false) {
                FieldRowViewViewModels[i].Backgrounds[j] = Brushes.Blue;
                ShotMatrix[i][j] = -1;
                NotifyPlayerTurn();
            }
        }
    }
}
