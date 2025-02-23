using DataHarbor.Common.Configuration;
using DataHarbor.Common.Messaging;
using DataHarbor.Common.Process;
using DataHarbor.Extractors.Commands;
using DataHarbor.Repository;
using System.Security.Cryptography;
using MassTransit;
using MediatR;
using System.Text;
using System.Runtime.Intrinsics.Arm;

namespace DataHarbor.Extractors
{
    /// <summary>
    ///  Data Extraction Message consumer.
    /// </summary>
    public class DataExtractionConsumer : IConsumer<ProcessMessage>
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

        public async Task Consume(ConsumeContext<ProcessMessage> messageContext)
        {
            _logger.LogInformation($"Received message: {messageContext.Message.FilePath} => {messageContext.Message.Status}");
            if (messageContext.Message.Status == ProcessMessageStatus.Ingest)
            {
                var processContext = InitializeProcessor(messageContext);

                // Read file.
                await _mediator.Send(new ReadFileQuery(processContext));

                // Map to the correct format.


                // publish final results
                await _mediator.Send(new ProcessRequestCommand(processContext));

             
                //var message = context.Message;
                //message.UniqueId = request.UniqueId;
                //message.Status = ProcessMessageStatus.TransferDispath;
                //await _messageBus.Publish(message);
            }
        }

        private ProcessContext InitializeProcessor(ConsumeContext<ProcessMessage> messageContext)
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
