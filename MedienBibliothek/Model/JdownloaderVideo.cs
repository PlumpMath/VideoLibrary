namespace MedienBibliothek.Model
{
    public class JdownloaderVideo
    {
        public JdownloaderVideo(string videoPath)
        {
            JdownloaderVideoPath = videoPath;
        }

        public string JdownloaderVideoPath { get; set; }
    }
}
