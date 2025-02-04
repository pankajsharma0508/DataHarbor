using DataHarbor.Common.Messaging;
using MassTransit;

namespace DataHarbor.Transformers
{
    public class DataTransformerConsumer : IConsumer<ProcessMessage>
    {
        private readonly ILogger<DataTransformerConsumer> _logger;
        private readonly IBus _messageBus;
        private readonly ITransformationService _transmissionService;

        public DataTransformerConsumer(ILogger<DataTransformerConsumer> logger, IBus messageBus,
            ITransformationService transmissionService)
        {
            _logger = logger;
            _messageBus = messageBus;
            _transmissionService = transmissionService;
        }

        public async Task Consume(ConsumeContext<ProcessMessage> context)
        {
            await _transmissionService.Transform(context.Message.UniqueId.ToString());

            context.Message.Status = ProcessMessageStatus.LoadSignal;
            await _messageBus.Publish(context.Message);
        }
    }
}
