using Newtonsoft.Json;

namespace lastFm.Models.LastFmResponseModels
{
    public class Track
    {
        public Artist Artist { get; set; }
        [JsonProperty("@attr")]
        public TrackAttributes Attr { get; set; }
        public string mbid { get; set; }
        public Album Album { get; set; }
        public string Streamable { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public Image[] Image { get; set; }

        [JsonProperty("date")]
        public Date Date { get; set; }
    }
}