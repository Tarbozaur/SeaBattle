using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle.Models {
    interface ICloseWindow {
        Action Close { get; set; }
    }
}
