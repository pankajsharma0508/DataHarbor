using DataHarbor.WebAPI.Models;
using MediatR;

namespace DataHarbor.WebAPI.Query
{
    public record GetAccountsQuery() : IRequest<List<Account>>;

    public record GetAccountQuery(string id) : IRequest<Account>;

    public record CreateAccountCommand(Account account) : IRequest<Account>;
}
