using DataHarbor.Common.Configuration;
using DataHarbor.Common.Models;
using DataHarbor.Repository;
using DataHarbor.WebAPI.Commands;
using MediatR;

namespace DataHarbor.WebAPI.Handlers
{
    public class CreateRequestHandler : IRequestHandler<CreateRequestCommand>
    {
        private readonly IRepository<ProcessRequest> repository;

        public CreateRequestHandler(IRepository<ProcessRequest> repository)
        {
            this.repository = repository;
        }
        public Task Handle(CreateRequestCommand command, CancellationToken cancellationToken)
        {
            return repository.Add(command.request);
        }
    }

    public class UpdateRequestHandler : IRequestHandler<UpdateRequestCommand>
    {
        private readonly IRepository<ProcessRequest> repository;

        public UpdateRequestHandler(IRepository<ProcessRequest> repository)
        {
            this.repository = repository;
        }
        public Task Handle(UpdateRequestCommand command, CancellationToken cancellationToken)
        {
            return repository.Update(command.request);
        }
    }

    public class DeleteRequestHandler : IRequestHandler<DeleteRequestCommand>
    {
        private readonly IRepository<ProcessRequest> repository;

        public DeleteRequestHandler(IRepository<ProcessRequest> repository)
        {
            this.repository = repository;
        }
        public Task Handle(DeleteRequestCommand command, CancellationToken cancellationToken)
        {
            return repository.Delete(command.id);
        }
    }

}
