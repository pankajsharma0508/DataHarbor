using DataHarbor.Common.Configuration;
using DataHarbor.Common.Messaging;
using DataHarbor.Common.Models;
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
        private readonly IRepository<ProcessRequest> _declarationRepository;
        private readonly IBus _messageBus;

        public DataExtractionConsumer(ILogger<DataExtractionConsumer> logger,
            IMediator mediator,
            IBus messageBus,
            IRepository<ProcessingConfiguration> configurationRepository,
            IRepository<ProcessRequest> declarationRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _messageBus = messageBus;
            _configurationRepository = configurationRepository;
            _declarationRepository = declarationRepository;
        }

        public async Task Consume(ConsumeContext<Anchored> messageContext)
        {
            try
            {
                
                _logger.LogInformation($"Received message: {messageContext.Message.DeclarationId}");

                var declarationId = messageContext.Message.DeclarationId;
                var processContext = await InitializeProcessor(messageContext.Message.DeclarationId);

                // Validate file
                await _mediator.Send(new ValidateRequestCommand(processContext));

                // Read file.
                await _mediator.Send(new ReadFileQuery(processContext));


                // publish final results
                await _mediator.Send(new ProcessRequestCommand(processContext));


                if (!processContext.ContainsCriticalError())
                {
                    var input = messageContext.Message;
                    var message = new Docked(declarationId);
                    await _messageBus.Publish(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to process Anchored message: {ex.Message}");
                _logger.LogError(ex.Message, ex);
            }
            finally
            {
                await messageContext.ConsumeCompleted;
            }
        }

        private async Task<ProcessContext> InitializeProcessor(Guid uniqueId)
        {
            var processContext = new ProcessContext();
            var declaration = await _declarationRepository.GetByID(uniqueId.ToString());
            if (declaration != null)
            {
                processContext.Configuration = GetProcessingConfiguration(declaration.Name);
                processContext.Declaration = declaration;
            }
            return processContext;
        }

        private ProcessingConfiguration GetProcessingConfiguration(string configurationName)
        {
            return _configurationRepository.FirstOrDefault(x => x.Name == configurationName).Result;
        }
    }
}
