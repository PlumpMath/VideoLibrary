using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MedienBibliothek.Interfaces;
using MedienBibliothek.Model;
using MedienBibliothek.View;

//using MedienBibliothek.View;

namespace MedienBibliothek.Controller
{
    public class JdownloaderDialogViewModel : INotifyPropertyChanged, ICommandHandler
    {
        readonly DirectoryInfo _jdownloaderPath = new DirectoryInfo(@Properties.Settings.Default.jdownloaderVideoPath);
        public static string MoviePath;
        //            MoviePath = moviePath;
        private string _qualityType;
        private string _jdownloaderMoviePath;
        private string _destinationFolderName;
        
        public  JdownloaderDialogViewModel(string path)
        {
            
            RenameAndMoveButton = "Rename and move";
            CheckBox720P = "720p";
            CheckBox1080P = "1080p";
            InitializeJdownloaderDialog(path);
//            _jdownloaderMoviePath = moviePath;
//            var jdownloaderDialog = new JdownloaderDialog();
//            jdownloaderDialog.Show();
        }
        
        #region Get/Set
        
        private string _jdownloaderRenameNameBox;
        public string JdownloaderRenameNameBox
        {
            get
            {
                return _jdownloaderRenameNameBox;
            }
            set
            {
                _jdownloaderRenameNameBox = value;
                OnPropertyChanged("JdownloaderRenameNameBox");
            }
        }

        public string RenameAndMoveButton { get; set; }
        public string CheckBox720P { get; set; }
        public string CheckBox1080P { get; set; }


        private bool _checkBox720PIsChecked;
        public bool CheckBox720PIsChecked
        {
            get
            {
                return _checkBox720PIsChecked;
            }
            set
            {
                _checkBox720PIsChecked = value;
                if (value)
                {
                    _qualityType = "720p";
                    CheckBox1080PIsChecked = false;

                }
                OnPropertyChanged("CheckBox720PIsChecked");
            }
        }

        private bool _checkBox1080PIsChecked;
        public bool CheckBox1080PIsChecked
        {
            get
            {
                return _checkBox1080PIsChecked;
            }
            set
            {
                _checkBox1080PIsChecked = value;
                if (value)
                {
                    _qualityType = "1080p";
                    CheckBox720PIsChecked = false;

                }
                OnPropertyChanged("CheckBox1080PIsChecked");
            }
        }

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

        private ICommand _copyVideoEnterKey;
        public ICommand CopyVideoEnterKey
        {
            get
            {
                if (null == _copyVideoEnterKey)
                {
                    _copyVideoEnterKey = new DelegateCommand(RenameAndMoveJdownloaderVideo);
                }
                return _copyVideoEnterKey;
            }
            set
            {
                _copyVideoEnterKey = value;
                OnPropertyChanged("CopyVideoEnterKey");
            }
        }

        private void CopyVideo()
        {
            throw new NotImplementedException();
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
            MoviePath = moviePath;
            CheckTheQuality(moviePath);
            if (_jdownloaderMoviePath == null)
                _jdownloaderMoviePath = moviePath;
//            var jdownloaderDialog = new JdownloaderDialog();
//            jdownloaderDialog.Show();


        }


        private void RenameAndMoveJdownloaderVideo()
        {
            
            var pathString = MoviePath;
            _jdownloaderMoviePath = MoviePath;

            

            int index = pathString.LastIndexOf("\\", System.StringComparison.Ordinal);
            if (index > 0)
                pathString = pathString.Substring(0, index);
            
            _destinationFolderName = pathString + "\\" + JdownloaderRenameNameBox + " " + _qualityType;
            Directory.Move(_jdownloaderMoviePath, _destinationFolderName);
            Directory.Move(_destinationFolderName, Properties.Settings.Default.videoPath+"\\"+EscapeDirName(_destinationFolderName));
            
            DeletingEmptyFolders(_jdownloaderPath.ToString());
            

        }

        private void DeletingEmptyFolders(string startPath)
        {
            
            

            foreach (var directory in Directory.GetDirectories(startPath))
            {
                DeletingEmptyFolders(directory);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
                    
            }
            
            
        }

        private void CheckTheQuality(string pathString)
        {
            if (pathString.Contains("720"))
            {
                _qualityType = "720p";
                CheckBox720PIsChecked = true;
            }
            if (pathString.Contains("1080"))
            {
                _qualityType = "1080p";
                CheckBox1080PIsChecked = true;
            }
        }

        private string EscapeDirName(string moviePath)
        {
            string[] folderCollection = moviePath.Split('\\');
            return folderCollection.Last();
        }

        public ICommand GetDoubleClickCommand()
        {
            return null;
        }

        public ICommand GetTextChangedCommand()
        {
            return null;
        }

        public ICommand GetReturnKeyEvent()
        {
            return CopyVideoEnterKey;
        }

        public ICommand GetCheckedVideo()
        {
            return null;
        }

    }
}
