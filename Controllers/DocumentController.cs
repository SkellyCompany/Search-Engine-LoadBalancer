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
        private readonly string baseUrl = "http://localhost:5000/document/";
        private readonly HttpClient client = new HttpClient();


        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            
            
            string html = baseUrl + id;
            HttpResponseMessage response = await client.GetAsync(html);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Document document = JsonConvert.DeserializeObject<Document>(result);
                return Ok(document);
            } else
            {
                int statCode = (int)response.StatusCode;
                string result = response.Content.ReadAsStringAsync().Result;
                return StatusCode(statCode, result);
            }
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllDocumentsFromDocTable()
        {
            string html = baseUrl + "all";
            HttpResponseMessage response = await client.GetAsync(html);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                List<Document> documents = JsonConvert.DeserializeObject<List<Document>>(result);
                return Ok(documents);
            } else
            {
                int statCode = (int)response.StatusCode;
                string result = response.Content.ReadAsStringAsync().Result;
                return StatusCode(statCode, result);
            }
        }
    }
}
