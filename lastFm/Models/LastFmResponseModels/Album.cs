using Newtonsoft.Json;

namespace lastFm.Models.LastFmResponseModels
{
    public class Album
    {
        public string mbid { get; set; }
        [JsonProperty("#text")]
        public string Text { get; set; }
    }
}