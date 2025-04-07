using DataHarbor.Common.Configuration;
using DataHarbor.WebAPI.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static DataHarbor.WebAPI.Query.ConfigurationQueries;

namespace DataHarbor.WebAPI.Controllers
{
    [Route("api/configuration")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("all")]
        public Task<List<ProcessingConfiguration>> GetConfigurations() => _mediator.Send(new GetConfigurationQuery());

        [HttpGet("{id}")]
        public Task<ProcessingConfiguration> Get(Guid id) => _mediator.Send(new GetConfigurationQueryById(id));

        [HttpPost("create")]
        public Task<ProcessingConfiguration> Post([FromBody] ProcessingConfiguration configuration) => _mediator.Send(new CreateConfigurationCommand(configuration));

        [HttpPost("update")]
        public Task<ProcessingConfiguration> Put([FromBody] ProcessingConfiguration configuration) => _mediator.Send(new UpdateConfigurationCommand(configuration));

        [HttpDelete("{id}")]
        public Task Delete(Guid id) => _mediator.Send(new DeleteConfigurationCommand(id));
    }
}
