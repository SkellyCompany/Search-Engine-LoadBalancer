using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SearchEngine.LoadBalancer.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SearchEngine.LoadBalancer.Controllers
{
    [Route("[controller]")]
    [ApiController]


    public class DocumentController : ControllerBase
    {
        private readonly HttpClient client = new HttpClient();


        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            string html = "http://localhost:5000/document/" + id;
            HttpResponseMessage response = await client.GetAsync(html);
            string result = await response.Content.ReadAsStringAsync();
            Document document = JsonConvert.DeserializeObject<Document>(result);
            return StatusCode((int)response.StatusCode, document); 
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllDocumentsFromDocTable()
        {
            string html = "http://localhost:5000/document/all";
            HttpResponseMessage response = await client.GetAsync(html);
            string result = await response.Content.ReadAsStringAsync();
			List<Document> documents = JsonConvert.DeserializeObject<List<Document>>(result);
            return StatusCode((int)response.StatusCode, documents);
        }
    }
}
