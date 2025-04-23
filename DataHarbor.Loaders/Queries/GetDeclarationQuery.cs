using DataHarbor.Common.Models;
using MediatR;

namespace DataHarbor.Loaders.Queries
{
    public record GetDeclarationQuery(Guid id) : IRequest<ProcessRequest>;
}
