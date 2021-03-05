using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
            HttpResponseMessage response = await client.GetAsync("document/id");
            if (response.IsSuccessStatusCode)
            {
                //product = await response.Content.ReadAsAsync<Product>();
            }
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllDocumentsFromDocTable()
        {
            return Ok();
        }
    }
}
