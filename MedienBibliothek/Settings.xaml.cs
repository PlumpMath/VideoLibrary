using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace MedienBibliothek
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private readonly FolderBrowserDialog browseVideoPath = new FolderBrowserDialog();
        private OpenFileDialog browseVlcExecPath = new OpenFileDialog();
        public Settings()
        {

            
            InitializeComponent();
            VideoFolderPathBox.Text = Properties.Settings.Default.videoPath;
            VlcExecPathBox.Text = Properties.Settings.Default.vlcFilePath;
        }

        

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(VideoFolderPathBox.Text))
            {
            }
            else
            {
                Properties.Settings.Default.videoPath = VideoFolderPathBox.Text;
            }
            if (String.IsNullOrEmpty(VlcExecPathBox.Text))
            {
            }
            else
            {
                Properties.Settings.Default.vlcFilePath = VlcExecPathBox.Text;
            }
            Properties.Settings.Default.Save();
            Close();

        }

        private void BrowseVideoPathClick(object sender, RoutedEventArgs e)
        {
            browseVideoPath.ShowDialog();
            VideoFolderPathBox.Text = browseVideoPath.SelectedPath;
        }

        private void BrowseVlcExecPathClick(object sender, RoutedEventArgs e)
        {
            browseVlcExecPath.ShowDialog();
            VlcExecPathBox.Text = browseVlcExecPath.InitialDirectory + browseVlcExecPath.FileName;
        }
    }
}
