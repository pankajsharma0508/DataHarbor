using DataHarbor.Common.Models;
using MediatR;

namespace DataHarbor.WebAPI.Query
{
    public record GetProcessRequestsQuery() : IRequest<List<ProcessRequest>>;

    public record GetProcessRequestQuery(string id) : IRequest<ProcessRequest>;

}
