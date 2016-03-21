namespace LikedAudioUploader.Classes
{
    public class LocalAudio
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }

        public LocalAudio(string a, string t, string f)
        {
            Artist = a;
            Title = t;
            FileName = f;
        }
    }
}