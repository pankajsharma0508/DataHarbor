using DataHarbor.Common.Configuration;
using MediatR;

namespace DataHarbor.WebAPI.Query
{
    public class ConfigurationQueries
    {
        public record GetConfigurationQueryById(Guid uniqueId) : IRequest<ProcessingConfiguration>;

        public record GetConfigurationQuery() : IRequest<List<ProcessingConfiguration>>;
    }
}
