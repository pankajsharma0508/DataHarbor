using DataHarbor.Common.Models;
using DataHarbor.WebAPI.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static DataHarbor.WebAPI.Query.ConfigurationQueries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataHarbor.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public Task<DataConfiguration> GetLatestConfiguration(string name) => _mediator.Send(new GetLatestConfigurationQuery(name));

        [HttpGet()]
        public Task<List<DataConfiguration>> GetConfigurations(string name) => _mediator.Send(new GetConfigurationQuery(name));


        // POST api/<ConfigurationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ConfigurationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ConfigurationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
