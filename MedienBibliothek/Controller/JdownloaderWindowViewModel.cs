using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MedienBibliothek.Model;
using MedienBibliothek.View;

namespace MedienBibliothek.Controller
{
    class JdownloaderWindowViewModel
    {
//        List<string> _folderCollection = new List<string>();  
        private string _jdownloaderMoviePath;
        private string _destinationMoviePathName;
        public JdownloaderWindowViewModel()
        {
            RenameAndMoveButton = "Rename and move";
            CheckBox720P = "720p";
            CheckBox1080P = "1080p";
        }
        
        #region Get/Set
        
        private string _jdownloaderRenameName;
        public string JdownloaderRenameName
        {
            get
            {
                return _jdownloaderRenameName;
            }
            set
            {
                _jdownloaderRenameName = value;
                OnPropertyChanged("JdownloaderRenameName");
            }
        }

        public string RenameAndMoveButton { get; set; }
        public string CheckBox720P { get; set; }
        public string CheckBox1080P { get; set; }

        private DelegateCommand _renameAndMoveCommand;
        public ICommand RenameAndMoveCommand
        {
            get
            {
                if (_renameAndMoveCommand == null)
                {
                    _renameAndMoveCommand = new DelegateCommand(RenameAndMoveJdownloaderVideo);
                }
                return _renameAndMoveCommand;
            }
        }

        

        #endregion Get/Set

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void InitializeJdownloaderDialog(string moviePath)
        {
            var jdownloaderDialog = new JdownloaderViewWindow();
            jdownloaderDialog.Show();
            _jdownloaderMoviePath = moviePath;
        }


        private void RenameAndMoveJdownloaderVideo()
        {
            _jdownloaderMoviePath =
                @"C:\\Jdownloader\\A.History.of.Violence.2005.7_StanleyTweedle2\\A.History.of.Violence.2005.German.720p.BluRay.x264-DETAiLS";
            _destinationMoviePathName = @"C:\\Jdownloader\\A.History.of.Violence.2005.7_StanleyTweedle2\\A History of Violence 720p";
            Directory.Move(_jdownloaderMoviePath, _destinationMoviePathName);
            Directory.Move(_destinationMoviePathName, Properties.Settings.Default.videoPath+"\\"+EscapeDirName(_destinationMoviePathName));

        }

        private string EscapeDirName(string moviePath)
        {
            string[] folderCollection = moviePath.Split('\\');
            return folderCollection.Last();
        }
    }
}
