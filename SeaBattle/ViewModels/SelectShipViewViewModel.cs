using SeaBattle.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace SeaBattle.ViewModels {
    public delegate void BattleWindowHandler(string i, string j, List<List<bool>> m1, List<List<bool>> m2, bool hasBot);
    internal class SelectShipViewViewModel : ViewModelBase, ICloseWindow {
        public event BattleWindowHandler NotifyBattleWindow = (string i, string j, List<List<bool>> m1, List<List<bool>> m2, bool hasBot) => { };
        public Action Close { get; set; }
        bool hasBot;
        int _ShipLength4 = 1;
        public int ShipLength4 {
            get => _ShipLength4;
            set => Set(ref _ShipLength4, value);
        }
        int _ShipLength3 = 2;
        public int ShipLength3{
            get => _ShipLength3;
            set => Set(ref _ShipLength3, value);
        }
        int _ShipLength2 = 3;
        public int ShipLength2 {
            get => _ShipLength2;
            set => Set(ref _ShipLength2, value);
        }
        int _ShipLength1 = 4;
        public int ShipLength1 {
            get => _ShipLength1;
            set => Set(ref _ShipLength1, value);
        }
        bool firstPlayer = true;
        string _Name = "";
        public string Name {
            get => _Name;
            set => Set(ref _Name, value);
        }
        string firstName = "";
        string secondName = "";
        List<List<bool>> FirstPlayerMatrix;
        List<List<bool>> SecondPlayerMatrix;
        FieldViewViewModel _FieldViewProperty;
        public FieldViewViewModel FieldViewProperty {
            get => _FieldViewProperty;
            set => Set(ref _FieldViewProperty, value);
        }
        public ICommand NextButtonCommand { get; }

        private bool CanNextButtonCommandExecute(object p) => true;
        private void OnNextButtonCommandExecuted(object p) {
            if (firstPlayer && ShipLength4 == 0 && ShipLength3 == 0 &&
                    ShipLength2 == 0 && ShipLength1 == 0) {
                ShipLength4 = 1;
                ShipLength3 = 2;
                ShipLength2 = 3;
                ShipLength1 = 4;
                for (int i = 0; i < 10; ++i) {
                    for (int j = 0; j < 10; ++j) {
                        FirstPlayerMatrix[i][j] = FieldViewProperty.IsShipMatrix[i][j];
                        FieldViewProperty.IsShipMatrix[i][j] = false;
                        FieldViewProperty.FieldRowViewViewModels[i].Backgrounds[j] = Brushes.White;
                    }
                }
                firstPlayer = false;
                firstName = Name;
                Name = "";
                if (hasBot) {
                    OnRandomButtonCommandExecuted(null);
                    firstPlayer = true;
                    for (int i = 0; i < 10; ++i) {
                        for (int j = 0; j < 10; ++j) {
                            SecondPlayerMatrix[i][j] = FieldViewProperty.IsShipMatrix[i][j];
                            FieldViewProperty.IsShipMatrix[i][j] = false;
                        }
                    }
                    NotifyBattleWindow(firstName, "Bot", FirstPlayerMatrix, SecondPlayerMatrix, hasBot);
                    Close();
                }
            } else if(!firstPlayer && ShipLength4 == 0 && ShipLength3 == 0 &&
                    ShipLength2 == 0 && ShipLength1 == 0) {
                ShipLength4 = 1;
                ShipLength3 = 2;
                ShipLength2 = 3;
                ShipLength1 = 4;
                for (int i = 0; i < 10; ++i) {
                    for (int j = 0; j < 10; ++j) {
                        SecondPlayerMatrix[i][j] = FieldViewProperty.IsShipMatrix[i][j];
                        FieldViewProperty.IsShipMatrix[i][j] = false;
                        FieldViewProperty.FieldRowViewViewModels[i].Backgrounds[j] = Brushes.White;
                    }
                }
                secondName = Name;
                Name = "";
                firstPlayer = true;
                NotifyBattleWindow(firstName, secondName, FirstPlayerMatrix, SecondPlayerMatrix, hasBot);
                Close();
            }
        }
        public ICommand RandomButtonCommand { get; }

        private bool CanRandomButtonCommandExecute(object p) => true;
        private void OnRandomButtonCommandExecuted(object p) {
            Random random = new Random();
            for (int i = 0; i < 10; ++i) {
                for (int j = 0; j < 10; ++j) {
                    FieldViewProperty.IsShipMatrix[i][j] = false;
                }
            }
            ShipLength4 = 1;
            ShipLength3 = 2;
            ShipLength2 = 3;
            ShipLength1 = 4;
            while (ShipLength4 != 0 || ShipLength3 != 0 ||
                    ShipLength2 != 0 || ShipLength1 != 0) {
                string emp = "";
                for (int i = 0; i < 10; ++i) {
                    for (int j = 0; j < 10; ++j) {
                        if (FieldViewProperty.IsShipMatrix[i][j]) {
                            emp += "1 ";
                        } else
                            emp += "0 ";
                    }
                    emp += "\n";
                }
                int row = random.Next(0, 10);
                int column = random.Next(0, 10);
                if (ShipLength4 != 0 || ShipLength3 != 0 || ShipLength2 != 0 || ShipLength1 != 0) {
                    int offset = 0;
                    if (ShipLength4 != 0) {
                        offset = 3;
                    } else if (ShipLength3 != 0) {
                        offset = 2;
                    } else if (ShipLength2 != 0) {
                        offset = 1;
                    } else if (ShipLength1 != 0) {
                        offset = 0;
                    }
                    int offset1 = offset;
                    bool seccess = false;
                    int koef1 = 1;
                    int koef2 = 0;
                    while (!seccess) {
                        int a1 = row >= row + koef1 * offset ? row + koef1 * offset : row;
                        int b1 = row < row + koef1 * offset ? row + koef1 * offset : row;
                        int a2 = column >= column + koef2 * offset1 ? column + koef2 * offset1 : column;
                        int b2 = column < column + koef2 * offset1 ? column + koef2 * offset1 : column;
                        if (a1 < 0 || a2 < 0 || b1 > 9 || b2 > 9) {
                            if (koef1 == 1) {
                                koef1 = -1;
                            } else if (koef1 == -1) {
                                koef1 = 0;
                                koef2 = 1;
                            }
                            if (koef2 == 1) {
                                koef2 = -1;
                            } else if (koef2 == -1) {
                                seccess = true;
                            }
                            continue;
                        }
                        int length = Math.Max(Math.Abs(a1 - b1) + 1, Math.Abs(a2 - b2) + 1);
                        if (Math.Min(Math.Abs(a1 - b1) + 1, Math.Abs(a2 - b2) + 1) != 1) {
                            return;
                        }
                        bool isValid = true;
                        int a1i = a1 > 0 ? 1 : 0;
                        int a2i = a2 > 0 ? 1 : 0;
                        int b1i = b1 < 10 - 1 ? 1 : 0;
                        int b2i = b2 < 10 - 1 ? 1 : 0;
                        for (int i = a1 - a1i; i <= b1 + b1i; ++i) {
                            for (int j = a2 - a2i; j <= b2 + b2i; ++j) {
                                if (FieldViewProperty.IsShipMatrix[i][j]) {
                                    isValid = false;
                                }
                            }
                        }
                        if (isValid) {

                            seccess = true;
                            if (length == 1 && ShipLength1 != 0) {
                                ShipLength1--;
                            } else if (length == 2 && ShipLength2 != 0) {
                                ShipLength2--;
                            } else if (length == 3 && ShipLength3 != 0) {
                                ShipLength3--;
                            } else if (length == 4 && ShipLength4 != 0) {
                                ShipLength4--;
                            } else {
                                return;
                            }
                            for (int i = a1; i < b1 + 1; ++i) {
                                for (int j = a2; j < b2 + 1; ++j) {
                                    FieldViewProperty.IsShipMatrix[i][j] = true;
                                }
                            }
                        } else {
                            if (koef1 == 1) {
                                koef1 = -1;
                            } else if (koef1 == -1) {
                                koef1 = 0;
                                koef2 = 1;
                            }
                            if (koef2 == 1) {
                                koef2 = -1;
                            } else if (koef2 == -1) {
                                seccess = true;
                            }
                        }
                    }
                }
            }
            if (hasBot && !firstPlayer)
                return;
            for (int i = 0; i < 10; ++i) {
                for (int j = 0; j < 10; ++j) {
                    if (FieldViewProperty.IsShipMatrix[i][j]) {
                        FieldViewProperty.FieldRowViewViewModels[i].Backgrounds[j] = Brushes.Orange;
                    } else {
                        FieldViewProperty.FieldRowViewViewModels[i].Backgrounds[j] = Brushes.White;
                    }
                }
            }
        }
        public ICommand AddButtonCommand { get; }

        private bool CanAddButtonCommandExecute(object p) => true;
        private void OnAddButtonCommandExecuted(object p) {
            if (FieldRowViewViewModel.lastBtnClicked != -1 && FieldRowViewViewModel.lastRowIndex != -1 &&
                    (ShipLength4 != 0 || ShipLength3 != 0 || ShipLength2 != 0 || ShipLength1 != 0)) {
                int a1 = FieldRowViewViewModel.lastRowIndex >= FieldRowViewViewModel.currentRowIndex ? FieldRowViewViewModel.currentRowIndex : FieldRowViewViewModel.lastRowIndex;
                int b1 = FieldRowViewViewModel.lastRowIndex < FieldRowViewViewModel.currentRowIndex ? FieldRowViewViewModel.currentRowIndex : FieldRowViewViewModel.lastRowIndex;
                int a2 = FieldRowViewViewModel.lastBtnClicked >= FieldRowViewViewModel.currentBtnClicked ? FieldRowViewViewModel.currentBtnClicked : FieldRowViewViewModel.lastBtnClicked;
                int b2 = FieldRowViewViewModel.lastBtnClicked < FieldRowViewViewModel.currentBtnClicked ? FieldRowViewViewModel.currentBtnClicked : FieldRowViewViewModel.lastBtnClicked;
                int length = Math.Max(Math.Abs(a1 - b1) + 1, Math.Abs(a2 - b2) + 1);
                if (Math.Min(Math.Abs(a1 - b1) + 1, Math.Abs(a2 - b2) + 1) != 1) {
                    return;
                }
                bool isValid = true;
                int a1i = a1 > 0 ? 1 : 0;
                int a2i = a2 > 0 ? 1 : 0;
                int b1i = b1 < 10 - 1 ? 1 : 0;
                int b2i = b2 < 10 - 1 ? 1 : 0;
                for (int i = a1 - a1i; i <= b1 + b1i; ++i) {
                    for (int j = a2 - a2i; j <= b2 + b2i; ++j) {
                        if (FieldViewProperty.IsShipMatrix[i][j]) {
                            isValid = false;
                        }
                    }
                }
                if (isValid) {
                    if (length == 1 && ShipLength1 != 0) {
                        ShipLength1--;
                    } else if (length == 2 && ShipLength2 != 0) {
                        ShipLength2--;
                    } else if (length == 3 && ShipLength3 != 0) {
                        ShipLength3--;
                    } else if (length == 4 && ShipLength4 != 0) {
                        ShipLength4--;
                    } else {
                        return;
                    }
                    for (int i = a1; i < b1 + 1; ++i) {
                        for (int j = a2; j < b2 + 1; ++j) {
                            FieldViewProperty.IsShipMatrix[i][j] = true;
                            FieldViewProperty.FieldRowViewViewModels[i].Backgrounds[j] = Brushes.Orange;
                        }
                    }
                }    
            }
        }
        public SelectShipViewViewModel(bool hasBot) {
            this.hasBot = hasBot;
            FieldViewProperty = new FieldViewViewModel();
            FirstPlayerMatrix = new List<List<bool>>();
            SecondPlayerMatrix = new List<List<bool>>();
            for (int i = 0; i < 10; ++i) {
                FirstPlayerMatrix.Add(new List<bool>());
                SecondPlayerMatrix.Add(new List<bool>());
                for (int j = 0; j < 10; ++j) {
                    FirstPlayerMatrix[i].Add(false);
                    SecondPlayerMatrix[i].Add(false);
                }
            }
            NextButtonCommand = new Command(OnNextButtonCommandExecuted, CanNextButtonCommandExecute);
            AddButtonCommand = new Command(OnAddButtonCommandExecuted, CanAddButtonCommandExecute);
            RandomButtonCommand = new Command(OnRandomButtonCommandExecuted, CanRandomButtonCommandExecute);
        }
    }
}
