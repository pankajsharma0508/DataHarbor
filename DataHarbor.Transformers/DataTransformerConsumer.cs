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
            try
            {
                _logger.LogInformation($"Received message: {messageContext.Message.DeclarationId}");

                // Fetch process request.
                var context = await PrepareProcessContext(messageContext.Message.DeclarationId);

                // Feed it to the pipeline.
                var pipeline = MailboxPipeline.GetPipeline();

                await pipeline.ExecuteAsync(context);

                // Store the result.
                var result = context.Declaration;
                result.Status = ProcessStatus.Adrifted;
                await _storageService.SaveResults(result);

                // trigger the message for next step.
                var msg = messageContext.Message;
                var message = new Adrifted(msg.DeclarationId);
                await _messageBus.Publish(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to process Docked message: {ex.Message}");
                _logger.LogError(ex.Message, ex);
            }
            finally
            {
                await messageContext.ConsumeCompleted;
            }
        }

        private async Task<ProcessContext> PrepareProcessContext(Guid requestId)
        {
            var context = new ProcessContext();

            // Fetch Process Request
            var processRequest = await _storageService.GetRequestByID(requestId);
            context.Declaration = processRequest;

            // Fetch configuration for the facility.
            var configuration = await _storageService.GetConfiguration(processRequest.Name);
            context.Configuration = configuration;

            return context;
        }
    }
}
