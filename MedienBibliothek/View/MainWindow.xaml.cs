using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            ((IDoubleClickCommandHolder)mainGridView.DataContext).GetDoubleClickCommand().Execute(null);
        

        }

        private void SettingsClickEvent(object sender, RoutedEventArgs e)
        {
            var settings = new Settings();
            settings.Show();
        }


        private void SearchBoxContextChanged(object sender, TextChangedEventArgs e)
        {
            ((IDoubleClickCommandHolder)mainGridView.DataContext).GetTextChangedCommand().Execute(e);
        }
    }
}
