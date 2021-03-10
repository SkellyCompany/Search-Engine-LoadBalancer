using SearchEngine.LoadBalancer.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchEngine.LoadBalancer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TermController : Controller
	{
		private readonly HttpClient client = new HttpClient();

        private readonly string baseUrl = "http://localhost:5000/term/";

		[HttpGet]
        public async Task<IActionResult> GetAllDocumentsForASpecificTerm([FromQuery] Request request)
        {
			string html = $"{baseUrl}?PageNumber={request.PageNumber}&PageCount={request.PageCount}&PageSize={request.PageSize}&Keyword={request.Keyword}";
			HttpResponseMessage response = await client.GetAsync(html);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                List<DocumentInTerm> terms = JsonConvert.DeserializeObject<List<DocumentInTerm>>(result);
                return Ok(terms);
            } else
            {
                int statCode = (int)response.StatusCode;
                string result = response.Content.ReadAsStringAsync().Result;
                return StatusCode(statCode, result);
            }
		}
    }
}
