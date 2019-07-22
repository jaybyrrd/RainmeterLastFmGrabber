using Newtonsoft.Json;

namespace lastFm.Models.LastFmResponseModels
{
    public class Image
    {
        public string Size { get; set; }
        [JsonProperty("#text")]
        public string Text { get; set; }
    }
}