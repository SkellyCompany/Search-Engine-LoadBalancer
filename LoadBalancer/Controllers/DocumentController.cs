using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SearchEngine.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllDocumentsFromDocTable()
        {
            return Ok();
        }
    }
}
