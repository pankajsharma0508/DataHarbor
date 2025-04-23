using DataHarbor.Common.Configuration;
using DataHarbor.Common.Models;
using DataHarbor.Common.Process;
using MediatR;

namespace DataHarbor.Loaders.Commands
{
    public record LoadResultsCommand(ProcessContext Context) : IRequest<ProcessContext>;

    public record UpdateAccountingBookCommand(ProcessContext Context) : IRequest<ProcessContext>;
}
