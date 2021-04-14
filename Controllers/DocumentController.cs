using SearchEngine.LoadBalancer.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LoadBalancer.LoadManager;
using System.Text;
using static SearchEngine.LoadBalancer.Entities.Log;
using SearchEngine.LoadBalancer.Domain.Pagination;
using System;

namespace SearchEngine.LoadBalancer.Controllers {

	[Route("[controller]")]
	[ApiController]
	public class DocumentController : Controller {

		private readonly ILoadManager _loadManager;
		private readonly HttpClient client = new HttpClient();

		public DocumentController(ILoadManager loadManager) {
			_loadManager = loadManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetByTerm([FromQuery] string term, [FromQuery] PaginationRequest paginationRequest) {
			string loggerUrl = "http://localhost:5002/log";
			string url = $"{_loadManager.GetNextHost()}" +
			$"/document/?PageNumber={paginationRequest.PageNumber}&PageCount={paginationRequest.PageCount}&PageSize={paginationRequest.PageSize}&term={term}";
			HttpResponseMessage response = await client.GetAsync(url);
			if (response.IsSuccessStatusCode) {
				Log log = new Log() {
					Type = LogType.SUCCESS,
					Url = url,
					Parameters = new Dictionary<string, string>() {
					   {"term", term}
					}
				};
				var body = new StringContent(JsonConvert.SerializeObject(log), Encoding.UTF8, "application/json");
				client.PostAsync(loggerUrl, body);
				string result = await response.Content.ReadAsStringAsync();
				List<Document> terms = JsonConvert.DeserializeObject<List<Document>>(result);
				return Ok(terms);
			} else {
				int statCode = (int)response.StatusCode;
				string result = response.Content.ReadAsStringAsync().Result;

				Log log = new Log() {
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
