using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MedienBibliothek.Interfaces;

namespace MedienBibliothek.Controller
{
    class RenameDialogViewModel : INotifyPropertyChanged, ICommandHandler
    {
        private string _qualityType;
        private string _renamedPath;

        #region Get/Set

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

        private string _renameNameBox;
        public string RenameNameBox
        {
            get
            {
                return _renameNameBox;
            }
            set
            {
                _renameNameBox = value;
                OnPropertyChanged("RenameNameBox");
            }
        }


        #endregion Get/Set

        public RenameDialogViewModel(string path)
        {
            InitializeRenameDialog(path);
            
        }

        private void InitializeRenameDialog(string path)
        {
            
            _renamedPath = path + "\\" + RenameNameBox + " " + _qualityType;
            var oldPath = new DirectoryInfo(path).ToString();
            Directory.Move(oldPath, _renamedPath);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
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
            return null;
        }

        public ICommand GetCheckedVideo()
        {
            return null;
        }
    }
}
