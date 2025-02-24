using DataHarbor.Common.Messaging;
using DataHarbor.Common.Models;
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
        private readonly IDbaseService<Transaction> _dbaseService;

        public DataLoaderConsumer(ILogger<Worker> logger, IMediator mediator, IDbaseService<Transaction> dbaseService)
        {
            _logger = logger;
            _mediator = mediator;
            _dbaseService = dbaseService;
        }

        public async Task Consume(ConsumeContext<Adrifted> messageContext)
        {
            _logger.LogInformation($"Received message: {messageContext.Message.FilePath}");

            //var result = await _mediator.Send(new GetProcessResultQuery(messageContext.Message.UniqueId.ToString()));
            //_dbaseService.CreateOrUpdateFile("D:\\DBFs\\inventor.dbf", result.Entries);
        }
    }
}
