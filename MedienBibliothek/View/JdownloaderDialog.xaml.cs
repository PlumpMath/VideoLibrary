using System.Windows;
using System.Windows.Input;
using MedienBibliothek.Controller;
using MedienBibliothek.Interfaces;

namespace MedienBibliothek.View
{

    public partial class JdownloaderDialog : Window
    {
        public JdownloaderDialog(string path)
        {
            InitializeComponent();
            DataContext = new JdownloaderDialogViewModel(path);
            SearchTextBox.Focusable = true;
            SearchTextBox.Focus();
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
