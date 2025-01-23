using DataHarbor.Common.Models;
using DataHarbor.Repository;
using DataHarbor.WebAPI.Commands;
using MediatR;

namespace DataHarbor.WebAPI.Handlers
{
    public class CreateConfigurationHandler : IRequestHandler<CreateConfigurationCommand, bool>
    {
        private readonly IRepository<DataConfiguration> repository;

        public CreateConfigurationHandler(IRepository<DataConfiguration> repository)
        {
            this.repository = repository;
        }
        public Task<bool> Handle(CreateConfigurationCommand command, CancellationToken cancellationToken)
        {
            return repository.Add(command.configuration);
        }
    }

    public class UpdateConfigurationHandler(IRepository<DataConfiguration> repository) : IRequestHandler<UpdateConfigurationCommand, bool>
    {
        private readonly IRepository<DataConfiguration> repository = repository;

        public Task<bool> Handle(UpdateConfigurationCommand command, CancellationToken cancellationToken)
        {
            return repository.Add(command.configuration);
        }
    }

}
