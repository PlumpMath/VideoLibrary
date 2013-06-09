using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using MedienBibliothek.Controller;



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
            var newMainTask = new MainWindowModel();
            var videoList = newMainTask.VideoList;
            
            var startVlc = new Process();
            startVlc.StartInfo.FileName = Properties.Settings.Default.vlcPath;
            startVlc.StartInfo.Arguments = "-v \"" + videoList[VideoListView.SelectedIndex].FullPath + "\"";
//            var test = videoList[VideoListView.SelectedItem].FullPath;
//            var keine = test;
//            startVlc.StartInfo.Arguments = "-v \"" + VideoListView.SelectedItems + "\"";
//            var test = VideoListView.SelectedItem .ToString();
           
//            startVlc.StartInfo.Arguments = "-v \"" + videoList.Select().FullPath + "\"";
            
            startVlc.Start();

           

        }

        private void Settings_Click_Event(object sender, RoutedEventArgs e)
        {
            var settings = new Settings();
            settings.Show();
        }

        


       
    }
}
