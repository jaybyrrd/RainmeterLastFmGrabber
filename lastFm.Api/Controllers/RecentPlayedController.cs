using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lastFm.Interfaces;
using lastFm.Models;
using Microsoft.AspNetCore.Mvc;

namespace lastFm.Api.Controllers
{
    [Route("api/RecentlyPlayed")]
    public class RecentPlayedController : Controller
    {
        private readonly ILastFmProvider _lastFmProvider;

        public RecentPlayedController(ILastFmProvider lastFmProvider)
        {
            _lastFmProvider = lastFmProvider;
        }

        [HttpGet("{user}")]
        public async Task<RecentlyPlayedResponse> Get([FromRoute] string user)
        {
            var song = (await _lastFmProvider.GetRecentTracks(user)).RecentTracks.Track[0];

            return new RecentlyPlayedResponse()
            {
                Album = song.Album.Text,
                Artist = song.Artist.Text,
                NowPlaying = song.Attr != null && song.Attr.NowPlaying.ToLower() == "true",
                Song = song.Name
            };
        }
    }
}
