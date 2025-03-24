using AutoMapper;
using DataHarbor.Common.Models;
using DataHarbor.Repository;
using DataHarbor.WebAPI.Commands;
using MassTransit;
using MediatR;

namespace DataHarbor.WebAPI.Handlers
{
    public class CreateRequestHandler : IRequestHandler<CreateRequestCommand>
    {
        private readonly ILogger<CreateRequestHandler> _logger;
        private readonly IBus _bus;

        public CreateRequestHandler(ILogger<CreateRequestHandler> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public Task Handle(CreateRequestCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Message Recieved for : {command.message.Name} and file name ${command.message.FilePath}", DateTimeOffset.Now);
            _bus.Publish(command.message);
            return Task.CompletedTask;
        }
    }

    public class UpdateRequestHandler : IRequestHandler<UpdateRequestCommand>
    {
        private readonly IRepository<ProcessRequest> repository;
        private readonly IMapper _mapper;

        public UpdateRequestHandler(IRepository<ProcessRequest> repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }
        public Task Handle(UpdateRequestCommand command, CancellationToken cancellationToken)
        {
            var declaration = _mapper.Map<ProcessRequest>(command.request);
            return repository.Update(declaration);
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
            return Task.CompletedTask;
            //return repository.Delete(command.id);
        }
    }

}
