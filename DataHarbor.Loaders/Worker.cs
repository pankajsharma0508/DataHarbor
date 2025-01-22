using DataHarbor.Common.Models;
using DataHarbor.Loaders.Queries;
using DataHarbor.Loaders.Services;
using MediatR;

namespace DataHarbor.Loaders
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMediator _mediator;
        private readonly IDbaseService<Transaction> _dbaseService;

        public Worker(ILogger<Worker> logger, IMediator mediator, IDbaseService<Transaction> dbaseService)
        {
            _logger = logger;
            _mediator = mediator;
            _dbaseService = dbaseService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                var result = await _mediator.Send(new GetProcessResultQuery("9871f569-db22-4c1e-bfb6-4184989d7abb"));
                _dbaseService.CreateOrUpdateFile("D:\\DBFs\\inventor.dbf", result.Entries);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
