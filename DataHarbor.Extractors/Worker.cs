using DataHarbor.Extractors.Commands;
using MediatR;

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

                FileInfo fileInfo = new FileInfo("C:\\TestFiles\\sample.csv");
                if (fileInfo.Exists)
                {
                    var inputData = await _mediator.Send(new ReadFileQuery(fileInfo.FullName, fileInfo.Extension));
                    var request = new Common.Models.ProcessRequest
                    {
                        UniqueId = Guid.NewGuid(),
                        Name = fileInfo.Name,
                        Description = fileInfo.Name,
                        Entries = inputData,
                        RecieveDate = fileInfo.CreationTime
                    };
                    await _mediator.Send(new ProcessRequestCommand(request));
                }

                await Task.Delay(1000000, stoppingToken);
            }
        }
    }
}
