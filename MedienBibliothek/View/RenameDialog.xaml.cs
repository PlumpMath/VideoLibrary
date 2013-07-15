using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MedienBibliothek.Controller;
using MedienBibliothek.Interfaces;

namespace MedienBibliothek.View
{
    public partial class RenameDialog : Window
    {
        public RenameDialog(string path)
        {
            InitializeComponent();
            DataContext = new RenameDialogViewModel(path);
            RenameTextBox.Focusable = true;
            RenameTextBox.Focus();
        }

        private void PressKeyEventInTextbox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ((ICommandHandler)mainGridView.DataContext).GetReturnKeyEvent().Execute(e);
                Close();
            }
            
        }

        private void CloseWindowClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

