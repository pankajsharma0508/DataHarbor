using DataHarbor.Common.Process;
using MediatR;
using System.Dynamic;

namespace DataHarbor.Extractors.Commands
{
    public record ReadFileQuery(ProcessContext context) : IRequest<ProcessContext>;
}
