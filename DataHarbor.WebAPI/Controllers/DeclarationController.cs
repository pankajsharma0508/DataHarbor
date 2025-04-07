using DataHarbor.Common.Messaging;
using DataHarbor.WebAPI.Commands;
using DataHarbor.WebAPI.Models;
using DataHarbor.WebAPI.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataHarbor.WebAPI.Controllers
{
    [Route("api/declaration")]
    [ApiController]
    public class DeclarationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DeclarationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public Task<List<Declaration>> Get() => _mediator.Send(new GetProcessRequestsQuery());

        [HttpGet("{id}")]
        public Task<Declaration> Get(string id) => _mediator.Send(new GetProcessRequestQuery(id));

        [HttpPost("create")]
        public Task<Declaration> Post([FromBody] Declaration request) => _mediator.Send(new CreateDeclarationCommand(request));

        [HttpPut("update")]
        public Task Put([FromBody] Declaration request) => _mediator.Send(new UpdateDeclarationCommand(request));

        [HttpDelete("{id}")]
        public Task Delete(string id) => _mediator.Send(new DeleteDeclarationCommand(id));
    }
}
