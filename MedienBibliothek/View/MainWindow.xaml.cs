using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MedienBibliothek.Controller;
using MedienBibliothek.Interfaces;
using MedienBibliothek.Model;


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
            settings.ShowDialog();
            
        }


        private void SearchBoxContextChanged(object sender, TextChangedEventArgs e)
        {
            ((ICommandHandler)mainGridView.DataContext).GetTextChangedCommand().Execute(e);
        }

        private void PressKeyEventInTextbox(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                ((ICommandHandler)mainGridView.DataContext).GetReturnKeyEvent().Execute(e);
            }
        }

//    

//        private void CheckedVideo(object sender, RoutedEventArgs e)
//        {
//            ((ICommandHandler)mainGridView.DataContext).GetCheckedVideo().Execute(e);
//            VideoListView.SelectedItems.Add(e.Source);
//        }
        private void ItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((MainViewModel)mainGridView.DataContext).SelectedVideos = VideoListView.SelectedItems;
            
        }
    }
}
