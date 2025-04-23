using DataHarbor.Common.Messaging;
using MassTransit;

namespace DataHarbor.Extractors
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
                await _bus.Publish(new Anchored(Guid.NewGuid()));
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}
