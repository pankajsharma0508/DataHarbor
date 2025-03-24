using DataHarbor.Common.Configuration;
using DataHarbor.Common.Models;
using MediatR;

namespace DataHarbor.Loaders.Commands
{
    public record LoadResultsCommand(ProcessingConfiguration Configuration, ProcessRequest Request) : IRequest<bool>;
}
