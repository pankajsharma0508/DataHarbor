using DataHarbor.Common.Configuration;
using MediatR;

namespace DataHarbor.WebAPI.Commands
{
    public record CreateConfigurationCommand(ProcessingConfiguration configuration) : IRequest<ProcessingConfiguration>;

    public record UpdateConfigurationCommand(ProcessingConfiguration configuration) : IRequest<ProcessingConfiguration>;

    public record DeleteConfigurationCommand(Guid configurationId) : IRequest;
}
