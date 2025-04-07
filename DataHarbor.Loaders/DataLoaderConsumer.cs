using DataHarbor.Common.Messaging;
using DataHarbor.Common.Models;
using DataHarbor.Loaders.Commands;
using DataHarbor.Loaders.Queries;
using DataHarbor.Loaders.Services;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                // Fetch transactions
                var declaration = await _mediator.Send(new GetDeclarationQuery(messageContext.Message.DeclarationId));

                // Fetch configuration before progressing
                var configuration = await _mediator.Send(new ProcessingConfigurationQuery(declaration.Name));

                // Create or Update Dbase file
                if (declaration != null)
                {
                    await _mediator.Send(new UpdateAccountingBookCommand(declaration));
                    await _mediator.Send(new LoadResultsCommand(configuration, declaration));
                }
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
    }
}
