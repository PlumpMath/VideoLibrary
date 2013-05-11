using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MedienBibliothek.Model;

namespace MedienBibliothek.Controller
{
    class MainWindowModel : INotifyPropertyChanged
    {

        public MainWindowModel()
        {
            RefreshButtonName = "Refresh video list";
        }

        private string _refreshButtonName;
        public string RefreshButtonName
        {
            get
            {
                return _refreshButtonName;
            }
            set
            {
                _refreshButtonName = value;
                OnPropertyChanged("RefreshButtonName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        
        private DelegateCommand _refreshVideoListCommand;
        public ICommand RefreshVideoListCommand
        {
            get
            {
                if (_refreshVideoListCommand == null)
                {
                    _refreshVideoListCommand = new DelegateCommand(RefreshVideoList);
                }
                return _refreshVideoListCommand;
            }
        }

        private void RefreshVideoList()
        {
            RefreshButtonName = "Refreshing...";
        }
    
    
    
    }
}
