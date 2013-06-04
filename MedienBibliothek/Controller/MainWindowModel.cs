using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            InitialiseVideoList();
            //AddVideoToListView = new ObservableCollection<Video>(InitialiseVideoList());
            //            VideoName = new CollectionView(InitialiseVideoList());

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

        private ObservableCollection<Video> _videoList;
        public ObservableCollection<Video> VideoList
        {
            get
            {
                if (_videoList == null)
                {
                    _videoList = new ObservableCollection<Video>();
                }
                return _videoList;
            }
            set
            {
                _videoList = value;
                OnPropertyChanged("VideoList");
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

        private void ListView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
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
            InitialiseVideoList();
        }

        private void InitialiseVideoList()
        {
            VideoList = new ObservableCollection<Video>();
            if (videoPath.Exists)
            {
                FileInfo[] videoFiles = videoPath.GetFiles("*.mkv", SearchOption.AllDirectories);
                foreach (var videoFile in videoFiles)
                {
                    if (videoFile.Length >= 100000000)
                    {
                        VideoList.Add(GetVideoFromPath(videoFile.DirectoryName));
                        //listOfVideos.Add(SplitVideoFolderName(videoFile.DirectoryName).First().Key, SplitVideoFolderName(videoFile.DirectoryName).First().Value);
                    }

                }
            }

        }


        private string SplitVideoQuality(string videoNameWithQuality, string videoName)
        {
            string[] videoQuality = videoNameWithQuality.Split(new string[] { videoName }, StringSplitOptions.None);

            return videoQuality.Last();
        }

        private Video GetVideoFromPath(string fullDirectoryPath)
        {
            string[] videoNameWithQuality = fullDirectoryPath.Split(Path.DirectorySeparatorChar);
            string[] videoName = videoNameWithQuality.Last().Split(new string[] { "1080p", "720p" }, StringSplitOptions.RemoveEmptyEntries);
            string videoquality = SplitVideoQuality(videoNameWithQuality.Last(), videoName.First());
            return new Video(videoName.First(), videoquality, fullDirectoryPath);

        }



    }
}
