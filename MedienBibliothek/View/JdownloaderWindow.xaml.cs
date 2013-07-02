using System.Windows;
using MedienBibliothek.Controller;

namespace MedienBibliothek.View
{

    public partial class JdownloaderWindow : Window
    {
        public JdownloaderWindow(string path)
        {
      
            InitializeComponent();
            DataContext = new JdownloaderWindowViewModel(path);

        }


        private void CloseWindowClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
