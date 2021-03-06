using LoadBalancer.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoadBalancer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TermController : Controller
	{
		private readonly HttpClient client = new HttpClient();


		[HttpGet]
        public async Task<IActionResult> GetAllDocumentsForASpecificTerm([FromQuery] Request request)
        {
			string html = $"http://localhost:5000/term/?PageNumber={request.PageNumber}&PageCount={request.PageNumber}&PageSize={request.PageNumber}&Keyword={request.PageNumber}";
			HttpResponseMessage response = await client.GetAsync(html);
			string result = await response.Content.ReadAsStringAsync();
			List<Term> terms = JsonConvert.DeserializeObject<List<Term>>(result);
			return Ok(terms);
		}
    }
}
