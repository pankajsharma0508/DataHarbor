using DataHarbor.Common.Messaging;
using DataHarbor.Common.Models;
using DataHarbor.WebAPI.Commands;
using DataHarbor.WebAPI.Models;
using DataHarbor.WebAPI.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataHarbor.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeclarationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeclarationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<Declaration>> Get() => _mediator.Send(new GetProcessRequestsQuery());

        [HttpGet("{id}")]
        public Task<Declaration> Get(string id) => _mediator.Send(new GetProcessRequestQuery(id));

        [HttpPost]
        public Task Post([FromBody] Anchored message) => _mediator.Send(new CreateRequestCommand(message));

        [HttpPut]
        public Task Put([FromBody] Declaration request) => _mediator.Send(new UpdateRequestCommand(request));

        [HttpDelete("{id}")]
        public Task Delete(string id) => _mediator.Send(new DeleteRequestCommand(id));
    }
}
