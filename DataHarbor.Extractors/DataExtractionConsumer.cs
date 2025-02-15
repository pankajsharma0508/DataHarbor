using DataHarbor.Common.Configuration;
using DataHarbor.Common.Messaging;
using DataHarbor.Extractors.Commands;
using DataHarbor.Repository;
using MassTransit;
using MediatR;

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

        public async Task Consume(ConsumeContext<ProcessMessage> context)
        {
            Console.WriteLine($"Received message: {context.Message.FilePath} => {context.Message.Status}");
            if (context.Message.Status == ProcessMessageStatus.Ingest)
            {
                FileInfo fileInfo = new(context.Message.FilePath);
                if (fileInfo.Exists)
                {
                    var uniqueId = Guid.NewGuid();
                    var processingConfiguration = GetProcessingConfiguration(context.Message.Name);
                    var inputData = await _mediator.Send(new ReadFileQuery(processingConfiguration.OperatorFilesConfigurations, fileInfo.FullName));
                    var request = new Common.Models.ProcessRequest
                    {
                        UniqueId = uniqueId,
                        Id = uniqueId.ToString(),
                        Name = fileInfo.Name,
                        Description = fileInfo.Name,
                        Entries = inputData,
                        RecieveDate = fileInfo.CreationTime
                    };
                    await _mediator.Send(new ProcessRequestCommand(request));

                    var message = context.Message;
                    message.UniqueId = request.UniqueId;
                    message.Status = ProcessMessageStatus.TransferDispath;
                    await _messageBus.Publish(message);
                }
            }
        }

        private ProcessingConfiguration GetProcessingConfiguration(string configurationName)
        {
            return _configurationRepository.FirstOrDefault(x => x.Name == configurationName).Result;
        }
    }
}
