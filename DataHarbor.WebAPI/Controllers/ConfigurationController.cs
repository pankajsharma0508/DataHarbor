using DataHarbor.Common.Configuration;
using DataHarbor.WebAPI.Commands;
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
        public Task<ProcessingConfiguration> GetLatestConfiguration(string name)
            => _mediator.Send(new GetLatestConfigurationQuery(name));

        [HttpGet("configuration/all")]
        public Task<List<ProcessingConfiguration>> GetConfigurations(string name)
            => _mediator.Send(new GetConfigurationQuery(name));

        [HttpPost]
        public Task<bool> Post([FromBody] ProcessingConfiguration configuration)
            => _mediator.Send(new CreateConfigurationCommand(configuration));

        [HttpPut]
        public Task Put([FromBody] ProcessingConfiguration configuration)
            => _mediator.Send(new UpdateConfigurationCommand(configuration));

        [HttpDelete("{id}")]
        public Task Delete(string id)
            => _mediator.Send(new DeleteConfigurationCommand(id));
    }
}
