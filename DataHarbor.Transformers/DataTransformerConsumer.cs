using DataHarbor.Common.Messaging;
using MassTransit;

namespace DataHarbor.Transformers
{
    public class DataTransformerConsumer : IConsumer<Docked>
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

        public async Task Consume(ConsumeContext<Docked> messageContext)
        {
            _logger.LogInformation($"Received message: {messageContext.Message.FilePath}");

            //await _transmissionService.Transform(context.Message.UniqueId.ToString());

            var msg = messageContext.Message;
            var message = new Adrifted(msg.UniqueId, msg.Name, msg.FilePath, msg.RecievedOn);
            await _messageBus.Publish(message);
        }
    }
}
