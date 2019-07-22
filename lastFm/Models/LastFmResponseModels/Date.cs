using Newtonsoft.Json;

namespace lastFm.Models.LastFmResponseModels
{
    public class Date
    {
        public string uts { get; set; }
        [JsonProperty("#text")]
        public string Text { get; set; }
    }
}