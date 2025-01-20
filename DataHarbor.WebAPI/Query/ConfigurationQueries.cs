using DataHarbor.Common.Models;
using MediatR;

namespace DataHarbor.WebAPI.Query
{
    public class ConfigurationQueries
    {
        public record GetLatestConfigurationQuery(string name) : IRequest<DataConfiguration>;

        public record GetConfigurationQuery(string name) : IRequest<List<DataConfiguration>>;
    }
}
