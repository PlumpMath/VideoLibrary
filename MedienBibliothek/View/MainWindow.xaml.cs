using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MedienBibliothek.Controller;
using MedienBibliothek.Interfaces;


namespace MedienBibliothek.View
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ListViewDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((ICommandHandler)mainGridView.DataContext).GetDoubleClickCommand().Execute(null);
        

        }

        private void SettingsClickEvent(object sender, RoutedEventArgs e)
        {
            var settings = new Settings();
            settings.Show();
        }


        private void SearchBoxContextChanged(object sender, TextChangedEventArgs e)
        {
            ((ICommandHandler)mainGridView.DataContext).GetTextChangedCommand().Execute(e);
        }

        private void PressKeyEventInTextbox(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                ((ICommandHandler) mainGridView.DataContext).GetReturnKeyEvent().Execute(e);
            }
        }

//    

    }
}
