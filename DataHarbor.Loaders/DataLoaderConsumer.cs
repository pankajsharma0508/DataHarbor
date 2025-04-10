using DataHarbor.Common.Messaging;
using DataHarbor.Common.Process;
using DataHarbor.Loaders.Commands;
using DataHarbor.Loaders.Queries;
using MassTransit;
using MediatR;

namespace DataHarbor.Loaders
{
    public class DataLoaderConsumer : IConsumer<Adrifted>
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMediator _mediator;

        public DataLoaderConsumer(ILogger<Worker> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<Adrifted> messageContext)
        {
            try
            {
                _logger.LogInformation($"Received message: {messageContext.Message.DeclarationId}");
                var msg = messageContext.Message;
                var context = await InitializeProcessor(msg.DeclarationId);

                // Update accounting records
                await _mediator.Send(new UpdateAccountingBookCommand(context));

                // Update DBase File
                await _mediator.Send(new LoadResultsCommand(context));
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

        private async Task<ProcessContext> InitializeProcessor(Guid uniqueId)
        {
            var processContext = new ProcessContext();
            var declaration = await _mediator.Send(new GetDeclarationQuery(uniqueId)); ;
            if (declaration != null)
            {
                processContext.Declaration = declaration;
                processContext.Configuration = await _mediator.Send(new ProcessingConfigurationQuery(declaration.Name)); ;
            }
            return processContext;
        }
    }
}
