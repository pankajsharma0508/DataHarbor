using DataHarbor.Common.Configuration;
using DataHarbor.WebAPI.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DataHarbor.WebAPI.Query.ConfigurationQueries;

namespace DataHarbor.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConfigurationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("configuration/{uniqueId}")]
        public Task<ProcessingConfiguration> GetLatestConfiguration(Guid uniqueId)
            => _mediator.Send(new GetConfigurationQueryById(uniqueId));

        [HttpGet("configuration/all")]
        public Task<List<ProcessingConfiguration>> GetConfigurations() => _mediator.Send(new GetConfigurationQuery());

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
