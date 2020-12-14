using SeaBattle.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SeaBattle.ViewModels {
    class StatisticsViewViewModel : ViewModelBase {
        List<HistoryItem> _ListOfHistoryItems;
        public List<HistoryItem> ListOfHistoryItems {
            get => _ListOfHistoryItems;
            set => Set(ref _ListOfHistoryItems, value);
        }
        public StatisticsViewViewModel() {
            ListOfHistoryItems = new List<HistoryItem>();
            using (MyAppContext dBContext = new MyAppContext()) {
                ListOfHistoryItems = dBContext.History.ToList();
            }
        }

    }
}
