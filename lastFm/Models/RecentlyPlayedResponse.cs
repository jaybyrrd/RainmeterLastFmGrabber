namespace lastFm.Models
{
    public class RecentlyPlayedResponse
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Song { get; set; }
        public bool NowPlaying { get; set; }
    }
}
