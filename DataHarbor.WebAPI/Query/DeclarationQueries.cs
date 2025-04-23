using DataHarbor.WebAPI.Models;
using MediatR;

namespace DataHarbor.WebAPI.Query
{
    public record GetProcessRequestsQuery() : IRequest<List<Declaration>>;

    public record GetProcessRequestQuery(string id) : IRequest<Declaration>;

}
