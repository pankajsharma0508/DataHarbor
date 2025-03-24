using DataHarbor.Common.Configuration;
using MediatR;

namespace DataHarbor.Loaders.Queries
{
    public record ProcessingConfigurationQuery(string Name) : IRequest<ProcessingConfiguration>;
}
