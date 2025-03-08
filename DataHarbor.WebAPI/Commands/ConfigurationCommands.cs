using DataHarbor.Common.Configuration;
using MediatR;

namespace DataHarbor.WebAPI.Commands
{
    public record CreateConfigurationCommand(ProcessingConfiguration configuration) : IRequest<bool>;

    public record UpdateConfigurationCommand(ProcessingConfiguration configuration) : IRequest;

    public record DeleteConfigurationCommand(string configurationId) : IRequest;
}
