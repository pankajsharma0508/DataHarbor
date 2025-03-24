using DataHarbor.Common.Configuration;
using DataHarbor.Loaders.Queries;
using DataHarbor.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Loaders.Handlers
{
    public class ProcessingConfigurationQueryHandler : IRequestHandler<ProcessingConfigurationQuery, ProcessingConfiguration>
    {
        public IRepository<ProcessingConfiguration> _configurationRepository;

        public ProcessingConfigurationQueryHandler(IRepository<ProcessingConfiguration> configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public Task<ProcessingConfiguration> Handle(ProcessingConfigurationQuery request, CancellationToken cancellationToken)
        {
            return _configurationRepository.FirstOrDefault(x => x.Name == request.Name);
        }
    }
}
