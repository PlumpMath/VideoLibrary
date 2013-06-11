using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using MedienBibliothek.Interfaces;
using MedienBibliothek.Model;


namespace MedienBibliothek.Controller
{
   public class MainWindowModel : INotifyPropertyChanged, IDoubleClickCommandHolder
    {
       readonly DirectoryInfo _videoPath = new DirectoryInfo(@Properties.Settings.Default.videoPath);
       readonly DirectoryInfo _vlcPath = new DirectoryInfo(@Properties.Settings.Default.vlcPath);

       private Collection<Video> _originalVideoList;

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

       private Video _selectedVideo;
       public Video SelectedVideo
       {
           get
           {
               return _selectedVideo;
           }
           set
           {
               _selectedVideo = value;
               OnPropertyChanged("SelectedVideo");
           }
       }

       private ICommand _listViewDoubleClickCommand;
       public ICommand ListViewDoubleClickCommand
       {
           get
           {
               if(null == _listViewDoubleClickCommand)
               {
                   _listViewDoubleClickCommand = new DelegateCommand(HandleDoubleClickOnListItem);
               }
               return _listViewDoubleClickCommand;
           }
           set
           {
               _listViewDoubleClickCommand = value;
               OnPropertyChanged("ListViewDoubleClickCommand");
           }
       }

       private void HandleDoubleClickOnListItem()
       {
           var startVlc = new Process();
           startVlc.StartInfo.FileName = Properties.Settings.Default.vlcPath;
           startVlc.StartInfo.Arguments = "-v \"" + SelectedVideo.FullPath + "\"";
           startVlc.Start();
       }


       private ICommand _searchBoxChanged;
       public ICommand SearchBoxChanged
       {
           get
           {
               if(null == _searchBoxChanged)
               {
                   _searchBoxChanged = new DelegateCommand(InitialiseFindListView);
               }
               return _searchBoxChanged;
           }
           set
           {
               _searchBoxChanged = value;
               OnPropertyChanged("SearchBoxChanged");
           }
       }

       private ICommand _playVideoEnterKey;
       public ICommand PlayVideoEnterKey
       {
           get
           {
               if (null == _playVideoEnterKey)
               {
                   _playVideoEnterKey = new DelegateCommand(InitialiseFindListView);
               }
               return _playVideoEnterKey;
           }
           set
           {
               _playVideoEnterKey = value;
               OnPropertyChanged("PlayVideoEnterKey");
           }
       }

      
       private string _searchBoxContext;
       public string SearchBoxContext
       {
           get
           {
               return _searchBoxContext;
           }
           set
           {
               _searchBoxContext = value;
               OnPropertyChanged("SearchBoxContext");
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
           foreach (var video in _originalVideoList)
           {
               if(video.Name.ToLower().Contains(SearchBoxContext.ToLower()))
               {
                   filteredList.Add(video);
               }
               
           }
           
           VideoList = filteredList;
      
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
                    if (videoFile.Length >= 150000000)
                    {
                        VideoList.Add(GetVideoFromPath(videoFile.DirectoryName, videoFile.FullName));
                         
                    }

                }
            }
            _originalVideoList = new Collection<Video>(VideoList);

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


       public ICommand GetDoubleClickCommand()
       {
           return ListViewDoubleClickCommand;
       }

       public ICommand GetTextChangedCommand()
       {
           return SearchBoxChanged;
       }
    }
}
