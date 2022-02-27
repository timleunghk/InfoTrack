using InfoTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infotrack.Interfaces
{
    public interface IWebScrapeService
    {
       
        Task<List<SearchResult>> GetSearchResults(SearchCriteria searchCriteria);
    }
}
