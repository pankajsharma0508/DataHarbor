using DataHarbor.Common.Configuration;
using DataHarbor.Common.Messaging;
using DataHarbor.Common.Process;
using DataHarbor.Extractors.Commands;
using DataHarbor.Repository;
using MassTransit;
using MediatR;

namespace DataHarbor.Extractors
{
    /// <summary>
    ///  Data Extraction Message consumer.
    /// </summary>
    public class DataExtractionConsumer : IConsumer<Anchored>
    {
        private readonly ILogger<DataExtractionConsumer> _logger;
        private readonly IMediator _mediator;
        private readonly IRepository<ProcessingConfiguration> _configurationRepository;
        private readonly IBus _messageBus;

        public DataExtractionConsumer(ILogger<DataExtractionConsumer> logger,
            IMediator mediator,
            IBus messageBus,
            IRepository<ProcessingConfiguration> configurationRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _messageBus = messageBus;
            _configurationRepository = configurationRepository;
        }

        public async Task Consume(ConsumeContext<Anchored> messageContext)
        {
            _logger.LogInformation($"Received message: {messageContext.Message.FilePath}");
            var processContext = InitializeProcessor(messageContext);

            // Read file.
            await _mediator.Send(new ReadFileQuery(processContext));

            // Map to the correct format.


            // publish final results
            await _mediator.Send(new ProcessRequestCommand(processContext));

            var input = messageContext.Message;
            var message = new Docked(input.UniqueId, input.Name, input.FilePath, input.RecievedOn);
            await _messageBus.Publish(message);
        }

        private ProcessContext InitializeProcessor(ConsumeContext<Anchored> messageContext)
        {
            var filePath = messageContext.Message.FilePath;
            var processContext = new ProcessContext();
            processContext.Id = Guid.NewGuid();
            processContext.FilePath = filePath;
            processContext.Name = messageContext.Message.Name;

            var configurationName = messageContext.Message.Name;
            if (!string.IsNullOrEmpty(configurationName))
            {
                var processingConfiguration = GetProcessingConfiguration(configurationName);
                processContext.AddProcessingParameter(nameof(ProcessingConfiguration), processingConfiguration);
            }
            return processContext;
        }

        private ProcessingConfiguration GetProcessingConfiguration(string configurationName)
        {
            return _configurationRepository.FirstOrDefault(x => x.Name == configurationName).Result;
        }
    }
}
