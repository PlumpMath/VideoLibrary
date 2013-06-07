using System;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MedienBibliothek.Controller;
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
            var newMainTask = new MainWindowModel();
            var videoList = newMainTask.VideoList;
            
            var startVlc = new Process();
            startVlc.StartInfo.FileName = Properties.Settings.Default.vlcPath;
            startVlc.StartInfo.Arguments = "-v \"" + videoList[VideoListView.SelectedIndex].FullPath+"\"";
            startVlc.Start();

           

        }

        


       
    }
}
