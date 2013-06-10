﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MedienBibliothek.Interfaces
{
    interface IDoubleClickCommandHolder
    {
        ICommand GetDoubleClickCommand();
        ICommand GetTextChangedCommand();

    }
}
