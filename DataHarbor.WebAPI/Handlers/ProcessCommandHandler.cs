using AutoMapper;
using DataHarbor.Common.Messaging;
using DataHarbor.WebAPI.Commands;
using MassTransit;
using MediatR;

namespace DataHarbor.WebAPI.Handlers
{
    public class ProcessCommandHandler
    {
        public class DataProcessMessageHandler : IRequestHandler<DataProcessMessage>
        {
            private readonly ILogger<DataProcessMessageHandler> _logger;
            private readonly IBus _bus;
            private readonly IMapper _mapper;

            public DataProcessMessageHandler(ILogger<DataProcessMessageHandler> logger, IBus bus, IMapper mapper)
            {
                _logger = logger;
                _bus = bus;
                _mapper = mapper;
            }

            public Task Handle(DataProcessMessage command, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Message Recieved for : {command.message.DeclarationId}");
                switch (command.message.Status)
                {
                    case ProcessMessageStatus.Anchored:
                        _bus.Publish(_mapper.Map<Anchored>(command.message));
                        break;
                    case ProcessMessageStatus.Adrifted:
                        _bus.Publish(_mapper.Map<Adrifted>(command.message));
                        break;
                    case ProcessMessageStatus.Docked:
                        _bus.Publish(_mapper.Map<Docked>(command.message));
                        break;
                    default:
                        break;
                }
                return Task.CompletedTask;
            }
        }
    }
}
