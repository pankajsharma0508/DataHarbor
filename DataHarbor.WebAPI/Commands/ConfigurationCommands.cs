using DataHarbor.Common.Models;
using MediatR;

namespace DataHarbor.WebAPI.Commands
{
    public record CreateConfigurationCommand(DataConfiguration configuration) : IRequest<bool>;

    public record UpdateConfigurationCommand(DataConfiguration configuration) : IRequest<bool>;

    public record DeleteConfigurationCommand(DataConfiguration configuration) : IRequest<bool>;
}
