using DataHarbor.Common.Configuration;
using DataHarbor.Repository;
using DataHarbor.WebAPI.Commands;
using MediatR;

namespace DataHarbor.WebAPI.Handlers
{
    public class CreateConfigurationHandler : IRequestHandler<CreateConfigurationCommand, ProcessingConfiguration>
    {
        private readonly IRepository<ProcessingConfiguration> repository;

        public CreateConfigurationHandler(IRepository<ProcessingConfiguration> repository)
        {
            this.repository = repository;
        }
        public Task<ProcessingConfiguration> Handle(CreateConfigurationCommand command, CancellationToken cancellationToken)
        {
            return repository.Add(command.configuration);
        }
    }

    public class UpdateConfigurationHandler(IRepository<ProcessingConfiguration> repository) : IRequestHandler<UpdateConfigurationCommand, ProcessingConfiguration>
    {
        private readonly IRepository<ProcessingConfiguration> repository = repository;

        public Task<ProcessingConfiguration> Handle(UpdateConfigurationCommand command, CancellationToken cancellationToken)
        {
            return repository.Update(command.configuration);
        }
    }

    public class DeleteConfigurationHandler(IRepository<ProcessingConfiguration> repository) : IRequestHandler<DeleteConfigurationCommand>
    {
        private readonly IRepository<ProcessingConfiguration> repository = repository;

        public Task Handle(DeleteConfigurationCommand request, CancellationToken cancellationToken)
        {
            return repository.Delete(request.configurationId.ToString());
        }
    }

}
