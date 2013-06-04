using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedienBibliothek.Model
{
    public class Video
    {
        public Video(string videoName, string videoquality, string fullDirectoryPath, string fullPathOfVideo)
        {
            Name = videoName;
            Quality = videoquality;
            Path = fullDirectoryPath;
            FullPath = fullPathOfVideo;
        }

        public string Name { get; set; }
        public string Quality { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }

    }
}
