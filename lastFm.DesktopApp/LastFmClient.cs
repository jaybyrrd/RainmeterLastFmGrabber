using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace lastFm.DesktopApp
{
    public static class LastFmClient
    {
        public static RecentlyPlayedResponse GetRecentlyPlayedResponse(string user)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp("https://8b3f7j42lg.execute-api.us-east-1.amazonaws.com/Stage/api/recentlyplayed/" + user);
            var res = (HttpWebResponse) webRequest.GetResponse();
            using (var reader = new StreamReader(res.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                return JsonConvert.DeserializeObject<RecentlyPlayedResponse>(reader.ReadToEnd());
            }
        }
    }

    public class RecentlyPlayedResponse
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Song { get; set; }
        public bool NowPlaying { get; set; }
    }
}
