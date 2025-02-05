using DataHarbor.Common.Configuration;
using DataHarbor.Common.Models;
using DataHarbor.Repository;
using MediatR;
using static DataHarbor.WebAPI.Query.ConfigurationQueries;

namespace DataHarbor.WebAPI.Handlers
{
    public class GetConfigurationHandler : IRequestHandler<GetLatestConfigurationQuery, ProcessingConfiguration>
    {
        private readonly IRepository<ProcessingConfiguration> repository;
        
        public GetConfigurationHandler(IRepository<ProcessingConfiguration> repository)
        {
            this.repository = repository;
        }
        public Task<ProcessingConfiguration> Handle(GetLatestConfigurationQuery request, CancellationToken cancellationToken)
        {
            return repository.FirstOrDefault(x => x.Name == request.name);
        }
    }

    public class GetConfigurationsHandler : IRequestHandler<GetConfigurationQuery, List<ProcessingConfiguration>>
    {
        private readonly IRepository<ProcessingConfiguration> repository;

        public GetConfigurationsHandler(IRepository<ProcessingConfiguration> repository)
        {
            this.repository = repository;
        }
        public Task<List<ProcessingConfiguration>> Handle(GetConfigurationQuery request, CancellationToken cancellationToken)
        {
            return repository.Where(x => x.Name == request.name);
        }
    }
}
