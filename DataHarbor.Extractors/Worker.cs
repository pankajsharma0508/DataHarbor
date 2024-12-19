using DataHarbor.Extractors.Commands;
using DataHarbor.Extractors.Handlers;
using MassTransit.Mediator;

namespace DataHarbor.Extractors
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMediator _mediator;

        public Worker(ILogger<Worker> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                FileInfo fileInfo = new FileInfo("C:\\TestFiles\\sample.txt");
                if (fileInfo.Exists)
                {
                    await _mediator.Send(new ProcessFileCommand(fileInfo.Extension, fileInfo.FullName));
                }

                //await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
