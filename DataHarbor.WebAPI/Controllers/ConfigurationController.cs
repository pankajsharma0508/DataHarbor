using DataHarbor.Common.Models;
using DataHarbor.WebAPI.Commands;
using DataHarbor.WebAPI.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static DataHarbor.WebAPI.Query.ConfigurationQueries;

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

        [HttpGet("configuration/latest")]
        public Task<DataConfiguration> GetLatestConfiguration(string name) => _mediator.Send(new GetLatestConfigurationQuery(name));

        [HttpGet("configuration/all")]
        public Task<List<DataConfiguration>> GetConfigurations(string name) => _mediator.Send(new GetConfigurationQuery(name));

        [HttpPost]
        public Task<bool> Post([FromBody] DataConfiguration configuration) => _mediator.Send(new CreateConfigurationCommand(configuration));


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
