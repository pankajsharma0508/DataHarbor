using DataHarbor.WebAPI.Models;
using DataHarbor.WebAPI.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataHarbor.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public Task<List<Account>> Get() => _mediator.Send(new GetAccountsQuery());

        [HttpGet("{id}")]
        public Task<Account> Get(string id) => _mediator.Send(new GetAccountQuery(id));

        [HttpPost("create")]
        public Task<Account> Post([FromBody] Account account) => _mediator.Send(new CreateAccountCommand(account));

    }
}
