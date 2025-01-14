using DataHarbor.Common.Models;
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

        // GET: api/<ProcessRequestController>
        [HttpGet]
        public Task<List<ProcessRequest>> Get() => _mediator.Send(new GetProcessRequestsQuery());

        // GET api/<ProcessRequestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProcessRequestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProcessRequestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProcessRequestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
