using DataHarbor.Common.Messaging;
using MassTransit;

namespace DataHarbor.Transformers
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
                    await _bus.Publish(new Docked(new Guid("5d6d2c01-9793-42fd-9d33-7c632df01b73"), "W644", "C:\\TestFiles\\sample.csv", DateTime.UtcNow));
                }
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}
