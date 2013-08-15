using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using GongSolutions.Wpf.DragDrop;
using MedienBibliothek.Interfaces;
using MedienBibliothek.Model;
using MedienBibliothek.View;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using DragDropEffects = System.Windows.DragDropEffects;
using IDropTarget = GongSolutions.Wpf.DragDrop.IDropTarget;


namespace MedienBibliothek.Controller
{
   public class MainViewModel : INotifyPropertyChanged, ICommandHandler, IDropTarget, IDragSource
    {


       
       private Collection<Video> _originalVideoList;

       public MainViewModel()
       {
           RefreshButtonName = "Refresh video list";
           JdownloaderButtonName = "Get jdwonloader videos";
           CreateExcelFileButtonName = "Create excel file";
           SearchButtonName = "Search";
           ImageSourceUrl = "";
           InitialiseVideoList();
           
       }

       #region Get/Set

       private string _imageSourceUrl;
       public string ImageSourceUrl
       {
           get
           {
               return _imageSourceUrl;
           }
           set
           {
               _imageSourceUrl = value;
               OnPropertyChanged("ImageSourceUrl");
           }
       }

       private string _movieTitle;
       public string MovieTitle
       {
           get
           {
               return _movieTitle;
           }
           set
           {
               _movieTitle = value;
               OnPropertyChanged("MovieTitle");
           }
       }

       private string _movieReleaseDate;
       public string MovieReleaseDate
       {
           get
           {
               return _movieReleaseDate;
           }
           set
           {
               _movieReleaseDate = value;
               OnPropertyChanged("MovieReleaseDate");
           }
       }

       private string _moviePopularity;
       public string MoviePopularity
       {
           get
           {
               return _moviePopularity;
           }
           set
           {
               _moviePopularity = value;
               OnPropertyChanged("MoviePopularity");
           }
       }

       private string _movieVoteAverage;
       public string MovieVoteAverage
       {
           get
           {
               return _movieVoteAverage;
           }
           set
           {
               _movieVoteAverage = value;
               OnPropertyChanged("MovieVoteAverage");
           }
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
               if(value != null)
               {
                   _selectedVideo = value;
               }
               OnPropertyChanged("SelectedVideo");
           }
       }

       private IList _selectedVideos;
       public IList SelectedVideos
       {
           get
           {
               return _selectedVideos;
           }
           set
           {
               _selectedVideos = value;
               OnPropertyChanged("SelectedVideos");
           }
       }

       private JdownloaderVideo _selectedJdownloaderFile;
       public JdownloaderVideo SelectedJdownloaderFile
       {
           get
           {
               return _selectedJdownloaderFile;
           }
           set
           {
               _selectedJdownloaderFile = value;
               OnPropertyChanged("SelectedJdownloaderFile");
           }
       }

       private Video _checkedVideo;
       public Video CheckedVideo
       {
           get
           {
               return _checkedVideo;
           }
           set
           {
               _checkedVideo = value;
               OnPropertyChanged("CheckedVideo");
           }
       }

       private ICommand _listViewDoubleClickCommand;
       public ICommand ListViewDoubleClickCommand
       {
           get
           {
               if (null == _listViewDoubleClickCommand)
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

       private ICommand _searchBoxChanged;
       public ICommand SearchBoxChanged
       {
           get
           {
               if (null == _searchBoxChanged)
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
                   _playVideoEnterKey = new DelegateCommand(PlayFirstVideoInListView);
               }
               return _playVideoEnterKey;
           }
           set
           {
               _playVideoEnterKey = value;
               OnPropertyChanged("PlayVideoEnterKey");
           }
       }

       private ICommand _videoListSelectionChanged;
       public ICommand VideoListSelectionChanged
       {
           get
           {
               if (null == _videoListSelectionChanged)
               {
                   _videoListSelectionChanged = new DelegateCommand(RefreshMovieInformation);
               }
               return _videoListSelectionChanged;
           }
           set
           {
               _videoListSelectionChanged = value;
               OnPropertyChanged("VideoListSelectionChanged");
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

       private string _jdownloaderButtonName;
       public string JdownloaderButtonName
       {
           get
           {
               return _jdownloaderButtonName;
           }
           set
           {
               _jdownloaderButtonName = value;
               OnPropertyChanged("JdownloaderButtonName");
           }
       }

       private string _createExcelFileButtonName;
       public string CreateExcelFileButtonName
       {
           get
           {
               return _createExcelFileButtonName;
           }
           set
           {
               _createExcelFileButtonName = value;
               OnPropertyChanged("CreateExcelFileButtonName");
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

       private ObservableCollection<JdownloaderVideo> _jdownloaderVideoList;
       public ObservableCollection<JdownloaderVideo> JdownloaderVideoList
       {
           get
           {
               if (_jdownloaderVideoList == null)
               {
                   _jdownloaderVideoList = new ObservableCollection<JdownloaderVideo>();
               }
               return _jdownloaderVideoList;
           }
           set
           {
               _jdownloaderVideoList = value;
               OnPropertyChanged("JdownloaderVideoList");
           }
       }

       private ICommand _searchVideoListCommand;
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

       private ICommand _refreshVideoListCommand;
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

       private ICommand _refreshJdownloaderVideoListCommand;
       public ICommand RefreshJdownloaderVideoListCommand
       {
           get
           {
               if (_refreshJdownloaderVideoListCommand == null)
               {
                   _refreshJdownloaderVideoListCommand = new DelegateCommand(CheckForJdownloaderVideos);
               }
               return _refreshJdownloaderVideoListCommand;
           }
       }

       private ICommand _copyMoviesToUsbDrive;
       public ICommand CopyMoviesToUsbDrive
       {
           get
           {
               if (_copyMoviesToUsbDrive == null)
               {
                   _copyMoviesToUsbDrive = new DelegateCommand(CopyMoviesToUSBDrive);
               }
               return _copyMoviesToUsbDrive;
           }
       }

      

       private ICommand _renameMovie;
       public ICommand RenameMovie
       {
           get
           {
               if (_renameMovie == null)
               {
                   _renameMovie = new DelegateCommand(RenameVideoInList);
               }
               return _renameMovie;
           }
       }



       private DelegateCommand _createExcelFileCommand;
       public ICommand CreateExcelFileCommand
       {
           get
           {
               if (_createExcelFileCommand == null)
               {
                   _createExcelFileCommand = new DelegateCommand(InitialiseExcelFile);
               }
               return _createExcelFileCommand;
           }
       }

       #endregion Get/Set

       private void RefreshMovieInformation()
       {
           GetMovieInformations(_selectedVideo.Name);
       }

       private void GetMovieInformations(string movieName)
       {
           var baseUrl = "https://d3gtl9l2a4fn1j.cloudfront.net/t/p/w185";
           var client = new TMDbClient("9c51453ba783de3ed91ec927fe4b1ad3");
           SearchContainer<SearchMovie> results = client.SearchMovie(movieName);
           var backPathOfPoster = results.Results[0].PosterPath;
           ImageSourceUrl = baseUrl + backPathOfPoster;
           MovieTitle = results.Results[0].Title;
           MovieReleaseDate = results.Results[0].ReleaseDate.ToString();
           MoviePopularity = results.Results[0].Popularity.ToString();
           MovieVoteAverage = results.Results[0].VoteAverage.ToString();
       }

       private void RenameVideoInList()
       {
           if(SelectedVideo.Path != null)
           {
               var renameVideoDialog = new RenameDialog(SelectedVideo.Path);
               renameVideoDialog.ShowDialog();
               CheckForJdownloaderVideos();
               InitialiseVideoList();
           }
           
       }

       private void HandleDoubleClickOnListItem()
       {
           var startVlc = new Process();
           startVlc.StartInfo.FileName = Properties.Settings.Default.vlcFilePath;
           startVlc.StartInfo.Arguments = "-v \"" + SelectedVideo.FullPath + "\"";
           startVlc.Start();
       }

       private void CopyMoviesToUSBDrive()
       {
           var usbDriveChooseDialog = new FolderBrowserDialog();
           usbDriveChooseDialog.ShowDialog();
           var usbDrivePath = usbDriveChooseDialog.SelectedPath;
           foreach (var singleVideo in SelectedVideos)
           {
               var video = singleVideo as Video;
               if(video == null)
               {
                   return;
               } else
               {
                    CopySelectedMovies(video.Path, usbDrivePath, video.Name, video.Quality);
               }
               

           }

       }

       public static void CopySelectedMovies(string sourceDir, string destinationDir, string videoname, string videoquality)
       {
           var realDestinationPath = destinationDir + "\\" + videoname + videoquality;
           foreach (string dir in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
           {
               Directory.CreateDirectory(realDestinationPath + dir.Substring(sourceDir.Length));
           }

           foreach (string fileName in Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories))
           {
               File.Copy(fileName, realDestinationPath + fileName.Substring(sourceDir.Length));
           }
       }

       public event PropertyChangedEventHandler PropertyChanged;

       protected virtual void OnPropertyChanged(string propertyName)
       {
           PropertyChangedEventHandler handler = PropertyChanged;
           if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
       }


       

       private void InitialiseExcelFile()
       {
           var createExcelFile = new WriteExcelFileHelper();
           createExcelFile.WriteVideoListToExcelFile(VideoList);
       }

       private ObservableCollection<Video> filteredList;
       private void InitialiseFindListView()
       {
           filteredList = new ObservableCollection<Video>();
           foreach (var video in _originalVideoList)
           {
               if(video.Name.ToLower().Contains(SearchBoxContext.ToLower()))
               {
                   filteredList.Add(video);
               }
               
           }
           
           VideoList = filteredList;
           
       }

       private void PlayFirstVideoInListView()
       {
           if(filteredList.Count == 1)
           {
               var startVlc = new Process();
               startVlc.StartInfo.FileName = Properties.Settings.Default.vlcFilePath;
               startVlc.StartInfo.Arguments = "-v \"" + filteredList.First().FullPath + "\"";
               startVlc.Start();   
           }
         
       }

      

        public void CheckForNewVideos()
        {
            
            if(SearchBoxContext != "" || SearchBoxContext != null)
            {
                InitialiseVideoList();
                SearchBoxContext = "";
            }
            
        }

        private void CheckForJdownloaderVideos()
        {
            JdownloaderVideoList = new ObservableCollection<JdownloaderVideo>();
            var _jdownloaderVideoPath = new DirectoryInfo(@Properties.Settings.Default.jdownloaderVideoPath);
            if(_jdownloaderVideoPath.Exists)
            {
                FileInfo[] videoFiles = _jdownloaderVideoPath.GetFiles("*.mkv", SearchOption.AllDirectories);
                foreach (var videoFile in videoFiles)
                {
                    if (videoFile.Length >= 200000000)
                    {
                        if (videoFile.Directory != null)
                            JdownloaderVideoList.Add(new JdownloaderVideo(videoFile.Directory.ToString()));
                    }
                }
            }
        }

       public void InitialiseVideoList()
       {
           VideoList = new ObservableCollection<Video>();
           var _videoPath = new DirectoryInfo(@Properties.Settings.Default.videoPath);
           if (_videoPath.Exists)
           {
               FileInfo[] videoFiles = _videoPath.GetFiles("*.mkv", SearchOption.AllDirectories);
               foreach (var videoFile in videoFiles)
               {
                   if (videoFile.Length >= 200000000)
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

       public ICommand GetReturnKeyEvent()
       {
           return PlayVideoEnterKey;
       }

       public ICommand GetCheckedVideo()
       {
//           return CollectCheckedVideo;
           return null;
       }

       public ICommand GetSelecttionChangedEvent()
       {
           return VideoListSelectionChanged;
       }

       public void DragOver(IDropInfo dropInfo)
       {
           if (dropInfo.Data is MainViewModel || dropInfo.Data
           is JdownloaderVideo)
           {
               dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
               dropInfo.Effects = DragDropEffects.Move;
           }
       }

       public void Drop(IDropInfo dropInfo)
       {
           if(SelectedJdownloaderFile.JdownloaderVideoPath != null)
           {
               var jdownloaderDialog = new JdownloaderDialog(SelectedJdownloaderFile.JdownloaderVideoPath);
               jdownloaderDialog.ShowDialog();
               CheckForJdownloaderVideos();
               CheckForNewVideos();
           }
       }

       public void StartDrag(IDragInfo dragInfo)
       {
           var selectedVideo = (JdownloaderVideo)dragInfo.SourceItem;
           dragInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
           dragInfo.Data = selectedVideo;
          
       }

       public void Dropped(IDropInfo dropInfo)
       {

           
       }
    }
}
