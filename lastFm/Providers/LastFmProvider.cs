using System;
using System.Threading.Tasks;
using lastFm.Interfaces;
using lastFm.Models;
using lastFm.Models.LastFmResponseModels;
using Newtonsoft.Json;
using RestSharp;

namespace lastFm.Providers
{
    public class LastFmProvider : ILastFmProvider
    {
        private static LastFmConfig _config;
        public LastFmProvider(IRestClient client)
        {
            _config = new LastFmConfig()
            {
                ApiKey = Environment.GetEnvironmentVariable("LastFmSecret")
            };
        }

        public Task<LastFmResponse> GetRecentTracks(string user)
        {
            var client = new RestClient("http://ws.audioscrobbler.com/2.0/");
            var request = new RestRequest();
            Console.WriteLine("Config ApiKey: " + _config.ApiKey);
            request.AddParameter("user", user);
            request.AddParameter("api_key", _config.ApiKey);
            request.AddParameter("limit", 3);
            request.AddParameter("format", "json");
            request.AddParameter("method", "user.getrecenttracks");

            var result = client.Execute(request);
            return Task.FromResult(JsonConvert.DeserializeObject<LastFmResponse>(result.Content));
        }
    }
}
