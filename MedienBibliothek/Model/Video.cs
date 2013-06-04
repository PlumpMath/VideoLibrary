using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedienBibliothek.Model
{
    public class Video
    {
        public Video(string videoName, string videoquality, string fullDirectoryPath)
        {
            Name = videoName;
            Quality = videoquality;
            Path = fullDirectoryPath;
        }

        public string Name { get; set; }
        public string Quality { get; set; }
        public string Path { get; set; }


    }
}
