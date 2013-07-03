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
        private readonly FolderBrowserDialog _browseVideoPath = new FolderBrowserDialog();
        private readonly OpenFileDialog _browseVlcExecPath = new OpenFileDialog();

        private readonly FolderBrowserDialog _browseJdownloaderDownloadPath = new FolderBrowserDialog();
        private readonly OpenFileDialog _browseExcelFilePath = new OpenFileDialog();

        public Settings()
        {

            
            InitializeComponent();
            VideoFolderPathBox.Text = Properties.Settings.Default.videoPath;
            VlcExecPathBox.Text = Properties.Settings.Default.vlcFilePath;
            JdownloaderDownloadPathBox.Text = Properties.Settings.Default.jdownloaderVideoPath;
            ExcelFilePathBox.Text = Properties.Settings.Default.excelFile;
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
            if (String.IsNullOrEmpty(JdownloaderDownloadPathBox.Text))
            {
            }
            else
            {
                Properties.Settings.Default.jdownloaderVideoPath = JdownloaderDownloadPathBox.Text;
            }
            if (String.IsNullOrEmpty(ExcelFilePathBox.Text))
            {
            }
            else
            {
                Properties.Settings.Default.excelFile = ExcelFilePathBox.Text;
            }
            Properties.Settings.Default.Save();
            Close();

        }

        private void BrowseVideoPathClick(object sender, RoutedEventArgs e)
        {
            _browseVideoPath.ShowDialog();
            VideoFolderPathBox.Text = _browseVideoPath.SelectedPath;
        }

        private void BrowseVlcExecPathClick(object sender, RoutedEventArgs e)
        {
            _browseVlcExecPath.ShowDialog();
            VlcExecPathBox.Text = _browseVlcExecPath.InitialDirectory + _browseVlcExecPath.FileName;
        }

        private void BrowseExcelFilePathClick(object sender, RoutedEventArgs e)
        {
            _browseExcelFilePath.ShowDialog();
            ExcelFilePathBox.Text = _browseExcelFilePath.InitialDirectory + _browseExcelFilePath.FileName;
        }

        private void BrowseJdownloaderDownloadPathClick(object sender, RoutedEventArgs e)
        {
            _browseJdownloaderDownloadPath.ShowDialog();
            JdownloaderDownloadPathBox.Text = _browseJdownloaderDownloadPath.SelectedPath;
        }
    }
}
