using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SeaBattle.Models {
    public class HistoryItem {
        public int Id { get; set; }
        public string FirstPlayerName { get; set; }
        public string SecondPlayerName { get; set; }
        public string WinnerName { get; set; }
        public DateTime DateTime { get; set; }
        public HistoryItem() {
            Id = 0;
            FirstPlayerName = "Player 1";
            SecondPlayerName = "Player 2";
            WinnerName = "";
            DateTime = DateTime.Now;
        }
        public HistoryItem(int Id, string FirstPlayerName, string SecondPlayerName, string WinnerName, DateTime DateTime) 
        {
            this.Id = Id;
            this.FirstPlayerName = FirstPlayerName;
            this.SecondPlayerName = SecondPlayerName;
            this.WinnerName = WinnerName;
            this.DateTime = DateTime;
        }

    }
}
