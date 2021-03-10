using LoadBalancer.Entities;
using LoadBalancer.LoadBalancer;
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
		private readonly ILoadManager _loadManager;


		public TermController(ILoadManager loadManager)
		{
			_loadManager = loadManager;
		}

		[HttpGet]
        public async Task<IActionResult> GetAllDocumentsForASpecificTerm([FromQuery] Request request)
        {
			string html = $"{_loadManager.GetNextHost()}" +
			$"/term/?PageNumber={request.PageNumber}&PageCount={request.PageCount}&PageSize={request.PageSize}&Keyword={request.Keyword}";
			HttpResponseMessage response = await client.GetAsync(html);
			string result = await response.Content.ReadAsStringAsync();
			List<DocumentInTerm> terms = JsonConvert.DeserializeObject<List<DocumentInTerm>>(result);
			return Ok(terms);
		}
    }
}
