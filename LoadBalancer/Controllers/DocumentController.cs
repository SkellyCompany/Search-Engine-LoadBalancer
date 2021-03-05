using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SearchEngine.API.Controllers
{
    [Route("[controller]")]
    [ApiController]


    public class DocumentController : ControllerBase
    {
        private readonly HttpClient client = new HttpClient();


        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await client.GetAsync("http://localhost:5000/document/" + id);
            if (response.IsSuccessStatusCode)
            {
                //var product = await response.Content.ReadAsAsync<Product>();
            }
            var result = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<Document>(result);
            return Ok(res);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllDocumentsFromDocTable()
        {
            return Ok();
        }
    }

    public class Document {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
