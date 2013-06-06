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
   public class MainWindowModel : INotifyPropertyChanged
    {
       readonly DirectoryInfo _videoPath = new DirectoryInfo(@Properties.Settings.Default.videoPath);
       readonly DirectoryInfo _vlcPath = new DirectoryInfo(@Properties.Settings.Default.vlcPath);
       public MainWindowModel()
       {
           RefreshButtonName = "Refresh video list";
           SearchButtonName = "Search";
           InitialiseVideoList();
       }

       private string _searchButtonName;
       public string SearchButtonName
       {
           get
           {
               return _searchButtonName;
           }
           set
           {
               _searchButtonName = value;
               OnPropertyChanged("SearchButtonName");
           }
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        private DelegateCommand _searchVideoListCommand;
        public ICommand SearchVideoListCommand
        {
            get
            {
                if (_searchVideoListCommand == null)
                {
                    _searchVideoListCommand = new DelegateCommand(InitialiseFindListView);
                }
                return _searchVideoListCommand;
            }
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

       private void InitialiseFindListView()
       {
           var filteredList = new ObservableCollection<Video>();
           foreach (var video in VideoList)
           {
               if(video.Name.Contains("a"))
               {
                   filteredList.Add(video);
               }
           }
           VideoList = filteredList;
        
           filteredList.setFilterCRiterium(new NameFilter("a"));

            ListViewItem foundItem = VideoList.FindItemWithText(textBox1.Text, false, 0, true);
            if (foundItem != null)
            {
                listview1.TopItem = foundItem;
                listview1.TopItem.Selected = true;
                VideoList.
                
            }
       }

       private GetSearchedVideo()
       {
           
       }

        private void CheckForNewVideos()
        {
            InitialiseVideoList();
        }

        private void InitialiseVideoList()
        {
            VideoList = new ObservableCollection<Video>();
            if (_videoPath.Exists)
            {
                FileInfo[] videoFiles = _videoPath.GetFiles("*.mkv", SearchOption.AllDirectories);
                foreach (var videoFile in videoFiles)
                {
                    if (videoFile.Length >= 100000000)
                    {
                        VideoList.Add(GetVideoFromPath(videoFile.DirectoryName.Replace('_',' '),videoFile.FullName));
                         
                    }

                }
            }

        }


        private string SplitVideoQuality(string videoNameWithQuality, string videoName)
        {
            string[] videoQuality = videoNameWithQuality.Split(new string[] { videoName }, StringSplitOptions.None);

            return videoQuality.Last();
        }

        private Video GetVideoFromPath(string fullDirectoryPath, string fullPathOfVideo)
        {
            string[] videoNameWithQuality = fullDirectoryPath.Split(Path.DirectorySeparatorChar);
            string[] videoName = videoNameWithQuality.Last().Split(new string[] { "1080p", "720p" }, StringSplitOptions.RemoveEmptyEntries);
            string videoquality = SplitVideoQuality(videoNameWithQuality.Last(), videoName.First());
            return new Video(videoName.First(), videoquality, fullDirectoryPath, fullPathOfVideo);

        }



    }
}
