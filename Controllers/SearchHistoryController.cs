using SearchEngine.LoadBalancer.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LoadBalancer.LoadManager;
using System.Text;
using static SearchEngine.LoadBalancer.Entities.Log;

namespace SearchEngine.LoadBalancer.Controllers {
	[Route("[controller]")]
	[ApiController]
	public class SearchHistoryController : Controller {
		private readonly ILoadManager _loadManager;
		private readonly HttpClient client = new HttpClient();

		public SearchHistoryController(ILoadManager loadManager) {
			_loadManager = loadManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetHistory([FromQuery] string keyword, [FromQuery] int maxAmount) {
			string url = $"{_loadManager.GetNextHost()}/searchHistory/?" + "keyword=" + keyword + "&maxAmount=" + maxAmount;
			HttpResponseMessage response = await client.GetAsync(url);
			if (response.IsSuccessStatusCode) {
				string result = await response.Content.ReadAsStringAsync();
				SearchHistory searchHistory = JsonConvert.DeserializeObject<SearchHistory>(result);
				return Ok(searchHistory);
			} else {
				int statCode = (int)response.StatusCode;
				string result = response.Content.ReadAsStringAsync().Result;
				return StatusCode(statCode, result);
			}
		}
	}
}
