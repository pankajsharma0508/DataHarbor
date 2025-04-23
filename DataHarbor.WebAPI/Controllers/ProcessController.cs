using DataHarbor.Common.Messaging;
using DataHarbor.WebAPI.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataHarbor.WebAPI.Controllers
{
    [Route("api/process")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProcessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("send-message")]
        public Task Post([FromBody] ProcessMessage message) => _mediator.Send(new DataProcessMessage(message));
    }
}
