namespace LikedAudioUploader
{
    public class Audio
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }

        public Audio(string a, string t, string f)
        {
            Artist = a;
            Title = t;
            FileName = f;
        }
    }
}