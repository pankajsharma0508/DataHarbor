using DataHarbor.Common.Process;
using MediatR;

namespace DataHarbor.Extractors.Commands
{
    public record ProcessRequestCommand(ProcessContext Context) : IRequest;

    public record ValidateRequestCommand(ProcessContext Context): IRequest<ProcessContext>;
}
