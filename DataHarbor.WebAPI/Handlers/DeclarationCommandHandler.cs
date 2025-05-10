using AutoMapper;
using DataHarbor.Common.Messaging;
using DataHarbor.Common.Models;
using DataHarbor.Repository;
using DataHarbor.WebAPI.Commands;
using DataHarbor.WebAPI.Models;
using MassTransit;
using MediatR;

namespace DataHarbor.WebAPI.Handlers
{
    public class CreateDeclarationCommandHandler : IRequestHandler<CreateDeclarationCommand, Declaration>
    {
        private readonly ILogger<CreateDeclarationCommandHandler> _logger;
        private readonly IRepository<ProcessRequest> _repository;
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public CreateDeclarationCommandHandler(ILogger<CreateDeclarationCommandHandler> logger, IBus bus, IRepository<ProcessRequest> repository, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Declaration> Handle(CreateDeclarationCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Message Recieved for : {command.declaration.Name} and file description ${command.declaration.Description}", DateTimeOffset.Now);

            var declaration = _mapper.Map<ProcessRequest>(command.declaration);
            declaration.RecieveDate = DateTime.UtcNow;
            declaration.Status = ProcessStatus.Anchored;

            if (command.declaration.Files != null)
            {
                foreach (var file in command.declaration.Files)
                {
                    var attachment = new Attachment
                    {
                        FileName = file.FileName,
                        FileContentType = file.ContentType,
                        FileStream = file.OpenReadStream()
                    };
                    declaration.Attachments.Add(attachment);
                }
            }

            var added = await _repository.Add(declaration);
            await _bus.Publish(new Anchored(command.declaration.UniqueId));

            return _mapper.Map<Declaration>(added);
        }
    }

    public class UpdateDeclarationCommandHandler : IRequestHandler<UpdateDeclarationCommand, Declaration>
    {
        private readonly IRepository<ProcessRequest> repository;
        private readonly IMapper _mapper;

        public UpdateDeclarationCommandHandler(IRepository<ProcessRequest> repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }
        public async Task<Declaration> Handle(UpdateDeclarationCommand command, CancellationToken cancellationToken)
        {
            var declaration = _mapper.Map<ProcessRequest>(command.declaration);
            var updated = await repository.Update(declaration);
            return _mapper.Map<Declaration>(updated);
        }
    }

    public class DeleteDeclarationCommandHandler : IRequestHandler<DeleteDeclarationCommand>
    {
        private readonly IRepository<ProcessRequest> repository;

        public DeleteDeclarationCommandHandler(IRepository<ProcessRequest> repository)
        {
            this.repository = repository;
        }
        public Task Handle(DeleteDeclarationCommand command, CancellationToken cancellationToken)
        {
            return repository.Delete(command.id);
        }
    }

}
