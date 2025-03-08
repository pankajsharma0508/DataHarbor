using DataHarbor.Common.Constants;
using DataHarbor.Common.Messaging;
using DataHarbor.Common.Models;
using DataHarbor.Common.Process;
using DataHarbor.Transformers.Interfaces;
using DataHarbor.Transformers.Processors;
using MassTransit;

namespace DataHarbor.Transformers
{
    public class DataTransformerConsumer : IConsumer<Docked>
    {
        private readonly ILogger<DataTransformerConsumer> _logger;
        private readonly IBus _messageBus;
        private readonly IStorageService _storageService;

        public DataTransformerConsumer(ILogger<DataTransformerConsumer> logger, IBus messageBus, IStorageService storageService)
        {
            _logger = logger;
            _messageBus = messageBus;
            _storageService = storageService;
        }

        public async Task Consume(ConsumeContext<Docked> messageContext)
        {
            _logger.LogInformation($"Received message: {messageContext.Message.FilePath}");

            // Fetch process request.
            var context = await PrepareProcessContext(messageContext.Message.UniqueId);

            // Feed it to the pipeline.
            var pipeline = MailboxPipeline.GetPipeline();

            await pipeline.ExecuteAsync(context);

            // Store the result.
            //await _storageService.SaveResults(result);

            // trigger the message for next step.
            var msg = messageContext.Message;
            var message = new Adrifted(msg.UniqueId, msg.Name, msg.FilePath, msg.RecievedOn);
            await _messageBus.Publish(message);
        }

        private async Task<ProcessContext> PrepareProcessContext(Guid requestId)
        {
            var context = new ProcessContext();

            // Fetch Process Request
            var processRequest = await _storageService.GetRequestByID(requestId);
            context.AddProcessingParameter(ProcessingResultNames.RawData, processRequest);

            // Fetch configuration for the facility.
            var configuration = await _storageService.GetConfiguration(processRequest.Name);
            context.AddProcessingParameter(ProcessingResultNames.Configuration, configuration);

            return context;
        }
    }
}
