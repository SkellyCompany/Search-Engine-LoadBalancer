using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using LoadBalancer.Entities;

namespace LoadBalancer.Controllers
{
    [Route("[controller]")]
    [ApiController]


    public class DocumentController : ControllerBase
    {
        private readonly HttpClient client = new HttpClient();


        [HttpGet("{id:length(24)}")]
        public async Task<IRestResponse> GetById(string id)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("http://localhost:5002/document/");
            var request = new RestRequest(id, Method.GET);
            return client.Execute<Document>(request);
        }

        [HttpGet("all")]
        public async Task<IRestResponse> GetAllDocumentsFromDocTable()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("http://localhost:5002/document/all");
            var request = new RestRequest(Method.GET);
            return client.Execute<Document>(request);
        }
    }
}
