using SearchEngine.LoadBalancer.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LoadBalancer.LoadManager;
using System;

namespace SearchEngine.LoadBalancer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TermController : Controller
    {
        private readonly ILoadManager _loadManager;
        private readonly HttpClient client = new HttpClient();

        public TermController(ILoadManager loadManager)
        {
            _loadManager = loadManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocumentsForASpecificTerm([FromQuery] Request request)
        {
            Console.WriteLine("ass");
            string url = $"{_loadManager.GetNextHost()}" +
            $"/term/?PageNumber={request.PageNumber}&PageCount={request.PageCount}&PageSize={request.PageSize}&Keyword={request.Keyword}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                List<DocumentInTerm> terms = JsonConvert.DeserializeObject<List<DocumentInTerm>>(result);
                return Ok(terms);
            }
            else
            {
                int statCode = (int)response.StatusCode;
                string result = response.Content.ReadAsStringAsync().Result;
                return StatusCode(statCode, result);
            }
        }
    }
}
