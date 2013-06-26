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

        private void JdownloaderListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((ICommandHandler)mainGridView.DataContext).GetSelectionChangedEvent().Execute(e);
        }


//        private void SearchAreaGrid_PatientResults_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
//        {
//         ListBox parent = (ListBox)sender;
//            var dragSource = parent;
//
//            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));
//
//            if (data != null)
//            {
//                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
//            }
//        }
//
//
//
//        private static object GetDataFromListBox(ListBox source, Point point)
//        {
//            UIElement element = source.InputHitTest(point) as UIElement;
//            if (element != null)
//            {
//                object data = DependencyProperty.UnsetValue;
//                while (data == DependencyProperty.UnsetValue)
//                {
//                    data = source.ItemContainerGenerator.ItemFromContainer(element);
//                    if (data == DependencyProperty.UnsetValue)
//                    {
//                        element = VisualTreeHelper.GetParent(element) as UIElement;
//                    }
//                    if (element == source)
//                    {
//                        return null;
//                    }
//                }
//                if (data != DependencyProperty.UnsetValue)
//                {
//                    return data;
//                }
//            }
//            return null;
//        }


    }
}
