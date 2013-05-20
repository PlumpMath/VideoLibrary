using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MedienBibliothek.Model;


namespace MedienBibliothek.Controller
{
    class MainWindowModel : INotifyPropertyChanged
    {

        DirectoryInfo videoPath = new DirectoryInfo(@Properties.Settings.Default.videoPath);
        public MainWindowModel()
        {
            RefreshButtonName = "Refresh video list";
            AddVideoToListView = new CollectionView(GetAvailableVideos());
//            VideoName = new CollectionView(GetAvailableVideos());
            
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

        private CollectionView _videoName;
        public CollectionView VideoName
        {
            get
            {
                return _videoName;
            }
            set
            {
                _videoName = value;
                OnPropertyChanged("VideoName");
            }
        }
        

        private CollectionView _addVideoToListView;
        public CollectionView AddVideoToListView
        {
            get
            {
                return _addVideoToListView;
            }
            set
            {
                _addVideoToListView = value;
                OnPropertyChanged("AddVideoToListView");
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
                    _refreshVideoListCommand = new DelegateCommand(CheckForNewVideos);
                }
                return _refreshVideoListCommand;
            }
        }

        private void CheckForNewVideos()
        {
            GetAvailableVideos();
        }

        private Dictionary<string,string> GetAvailableVideos()
        {
            var listOfVideos = new Dictionary<string, string>();
            if(videoPath.Exists)
            {
                FileInfo[] videoFiles = videoPath.GetFiles("*.mkv", SearchOption.AllDirectories);
                foreach (var videoFile in videoFiles)
                {
                    if(videoFile.Length >= 100000000)
                    {
                        listOfVideos.Add(SplitVideoFolderName(videoFile.DirectoryName).First().Key, SplitVideoFolderName(videoFile.DirectoryName).First().Value);
                        
                    }
                    
                }
            }

            return listOfVideos;
        } 

        private string SplitVideoQuality(string videoNameWithQuality, string videoName)
        {
            string[] videoQuality = videoNameWithQuality.Split(new string[] { videoName }, StringSplitOptions.None);

            return videoQuality.Last();
        }

        private Dictionary<string,string> SplitVideoFolderName(string fullDirectoryPath)
        {
            Dictionary<string,string> videoInfo = new Dictionary<string, string>();

            string[] videoNameWithQuality = fullDirectoryPath.Split(Path.DirectorySeparatorChar);
            string[] videoName = videoNameWithQuality.Last().Split(new string[] {"1080p" , "720p"}, StringSplitOptions.RemoveEmptyEntries);
            string videoquality = SplitVideoQuality(videoNameWithQuality.Last(), videoName.First());
            videoInfo.Add(videoName.First(), videoquality);
            return videoInfo;

        }
    
    
    
    }
}
