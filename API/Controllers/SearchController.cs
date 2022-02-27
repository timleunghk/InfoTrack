using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Infotrack.Interfaces;
using InfoTrack.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infotrack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly IWebScrapeService _service;



        public SearchController(IWebScrapeService service)
        {
            _service = service;
        }

        //TODO GetResultCount return number of records count

        [HttpPost]
        public async Task<JsonResult> GetResults(string keywords, string targetUrl)
        {
            var searchCriteria = new SearchCriteria() { Keywords = keywords, TargetUrl = targetUrl };
            List<SearchResult> searchResults;

            try
            {
                searchResults = await _service.GetSearchResults(searchCriteria);
            }
            catch (HttpRequestException ex)
            {
                return Json(new { Success = "False", ResponseText = ex.Message });
            }
    
            var result = new SearchViewModel() { Keywords = keywords, TargetUrl = targetUrl, SearchResults = searchResults };
           
            return Json(result);
       
        }
  
    }
}
