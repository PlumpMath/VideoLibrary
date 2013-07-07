using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MedienBibliothek.Interfaces
{
    interface ICommandHandler
    {
        ICommand GetDoubleClickCommand();
        ICommand GetTextChangedCommand();
        ICommand GetReturnKeyEvent();
        ICommand GetCheckedVideo();
        
    }
}
