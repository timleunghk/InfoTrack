using Infotrack.Interfaces;
using InfoTrack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace Infotrack.Services
{
    public class WebScrapeService : IWebScrapeService
    {
        private readonly IHtmlSourceService _service;

        public WebScrapeService(IHtmlSourceService service)
        {
            _service = service;
        }

        public async Task<List<SearchResult>> GetSearchResults(SearchCriteria searchCriteria)
        {
            string source = await _service.GetSource(searchCriteria.Keywords);

            List<SearchResult> results = ParseHtml(source, searchCriteria.TargetUrl);

            return results;
        }


        private List<SearchResult> ParseHtml(string source, string targetUrl)
        {
            var results = new List<SearchResult>();
            var resultNumber = 1;
            
            var startTag = "<div><div><div class=\"BNeawe s3v9rd AP7Wnd\">";
            var endTag = "</div>";
            var startTagIndex = source.IndexOf(startTag) + startTag.Length;
            var endTagIndex = source.IndexOf(endTag, startTagIndex);

            var startTagUrl = "<div class=\"BNeawe UPmit AP7Wnd lRVwie\">";
            var startTagUrlIndex = source.IndexOf(startTagUrl) + startTagUrl.Length;
            var endTagUrlIndex = source.IndexOf(endTag, startTagUrlIndex);

 

            var currentIndex = 1;

            while (resultNumber <= 100 && startTagIndex > currentIndex && endTagIndex > currentIndex)
            {
                string text = source.Substring(startTagIndex, endTagIndex - startTagIndex);
                text = HttpUtility.HtmlDecode(text).Replace(" › ", "/");

                string resultUrl = source.Substring(startTagUrlIndex, endTagUrlIndex - startTagUrlIndex);
                resultUrl = HttpUtility.HtmlDecode(resultUrl).Replace(" › ", "/");

                //Result: If resultUrl contains targetUrl enitity value then output JSON result here 2023/6/18
                if (resultUrl.Contains(targetUrl)) results.Add(new SearchResult() { Number = resultNumber, Url = resultUrl, Title = text });

                currentIndex = endTagIndex + 1;

                startTagIndex = source.IndexOf(startTag, currentIndex) + startTag.Length;
                endTagIndex = source.IndexOf(endTag, startTagIndex);

                startTagUrlIndex = source.IndexOf(startTagUrl, currentIndex) + startTagUrl.Length;
                endTagUrlIndex = source.IndexOf(endTag, startTagUrlIndex);

                resultNumber++;
            }

            return results;
        }
    }
}
