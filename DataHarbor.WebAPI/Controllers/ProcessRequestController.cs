using DataHarbor.Common.Models;
using DataHarbor.WebAPI.Commands;
using DataHarbor.WebAPI.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataHarbor.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcessRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<ProcessRequest>> Get() => _mediator.Send(new GetProcessRequestsQuery());

        [HttpGet("{id}")]
        public Task<ProcessRequest> Get(string id) => _mediator.Send(new GetProcessRequestQuery(id));

        [HttpPost]
        public Task Post([FromBody] ProcessRequest request) => _mediator.Send(new CreateRequestCommand(request));

        [HttpPut]
        public Task Put([FromBody] ProcessRequest request) => _mediator.Send(new UpdateRequestCommand(request));

        [HttpDelete("{id}")]
        public Task Delete(string id) => _mediator.Send(new DeleteRequestCommand(id));
    }
}
