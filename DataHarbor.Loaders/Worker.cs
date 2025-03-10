using DataHarbor.Common.Messaging;
using DataHarbor.Common.Models;
using DataHarbor.Loaders.Queries;
using DataHarbor.Loaders.Services;
using MassTransit;
using MediatR;

namespace DataHarbor.Loaders
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBus _bus;

        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                var message = new Adrifted(new Guid("73019e6e-6adf-4322-9a8d-1b6cfa20a997"), "W644", "", DateTime.UtcNow);
                await _bus.Publish(message);

                //var result = await _mediator.Send(new GetProcessResultQuery("9871f569-db22-4c1e-bfb6-4184989d7abb"));
                ////_dbaseService.CreateOrUpdateFile("D:\\DBFs\\inventor.dbf", result.Transactions);

                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}
