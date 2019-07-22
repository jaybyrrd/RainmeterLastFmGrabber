using Newtonsoft.Json;

namespace lastFm.Models.LastFmResponseModels
{
    public class RecentTracks
    {
        [JsonProperty("@attr")]
        public RecentTrackAttributes Attr { get; set; }
        public Track[] Track { get; set; }
    }
}