using SearchEngine.LoadBalancer.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LoadBalancer.LoadManager;
using System;
using SearchEngine.LoadBalancer.Entities;
using System.Text;
using static SearchEngine.LoadBalancer.Entities.Log;

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
            string loggerUrl = "http://localhost:5002/log";
            string url = $"{_loadManager.GetNextHost()}" +
            $"/term/?PageNumber={request.PageNumber}&PageCount={request.PageCount}&PageSize={request.PageSize}&Keyword={request.Keyword}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Log log = new Log()
                {
                    Type = LogType.SUCCESS,
                    Url = url,
                    Parameters = new Dictionary<string, string>() {
                       {"keyword", request.Keyword}
                    }
                };
                var body = new StringContent(JsonConvert.SerializeObject(log), Encoding.UTF8, "application/json");
                client.PostAsync(loggerUrl, body);

                string result = await response.Content.ReadAsStringAsync();
                List<DocumentInTerm> terms = JsonConvert.DeserializeObject<List<DocumentInTerm>>(result);
                return Ok(terms);
            }
            else
            {
                int statCode = (int)response.StatusCode;
                string result = response.Content.ReadAsStringAsync().Result;

                Log log = new Log()
                {
                    Type = LogType.ERROR,
                    Url = url,
                    Parameters = new Dictionary<string, string>() {
                       {"status_code", statCode.ToString()},
                       {"error_message", result}
                    }
                };
                var body = new StringContent(JsonConvert.SerializeObject(log), Encoding.UTF8, "application/json");
                client.PostAsync(loggerUrl, body);

                return StatusCode(statCode, result);
            }
        }
    }
}
