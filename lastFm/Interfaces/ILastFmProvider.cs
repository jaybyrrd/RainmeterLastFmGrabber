using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lastFm.Models;
using lastFm.Models.LastFmResponseModels;

namespace lastFm.Interfaces
{
    public interface ILastFmProvider
    {
        Task<LastFmResponse>  GetRecentTracks(string user);
    }
}
