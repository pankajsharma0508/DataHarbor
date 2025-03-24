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
            _logger.LogInformation($"Received message: {messageContext.Message.FilePath}");

            var msg = messageContext.Message;

            // Fetch configuration before progressing
            var configuration = await _mediator.Send(new ProcessingConfigurationQuery(msg.Name));

            // Fetch transactions
            var processedResult = await _mediator.Send(new GetTransactionsQuery(messageContext.Message.UniqueId));


            // Create or Update Dbase file
            if (processedResult != null)
            {
                await _mediator.Send(new LoadResultsCommand(configuration, processedResult));
            }
            await messageContext.ConsumeCompleted;
        }
    }
}
