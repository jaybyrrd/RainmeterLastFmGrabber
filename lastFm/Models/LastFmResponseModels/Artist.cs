using Newtonsoft.Json;

namespace lastFm.Models.LastFmResponseModels
{
    public class Artist
    {
        public string mbid { get; set; }
        [JsonProperty("#text")]
        public string Text { get; set; }
    }
}