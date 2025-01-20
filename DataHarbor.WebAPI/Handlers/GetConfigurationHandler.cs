using DataHarbor.Common.Models;
using DataHarbor.Repository;
using MediatR;
using static DataHarbor.WebAPI.Query.ConfigurationQueries;

namespace DataHarbor.WebAPI.Handlers
{
    public class GetConfigurationHandler : IRequestHandler<GetLatestConfigurationQuery, DataConfiguration>
    {
        private readonly IRepository<DataConfiguration> repository;

        public GetConfigurationHandler(IRepository<DataConfiguration> repository)
        {
            this.repository = repository;
        }
        public Task<DataConfiguration> Handle(GetLatestConfigurationQuery request, CancellationToken cancellationToken)
        {
            return repository.FirstOrDefault(x => x.Name == request.name);
        }
    }

    public class GetConfigurationsHandler : IRequestHandler<GetConfigurationQuery, List<DataConfiguration>>
    {
        private readonly IRepository<DataConfiguration> repository;

        public GetConfigurationsHandler(IRepository<DataConfiguration> repository)
        {
            this.repository = repository;
        }
        public Task<List<DataConfiguration>> Handle(GetConfigurationQuery request, CancellationToken cancellationToken)
        {
            return repository.Where(x => x.Name == request.name);
        }
    }
}
