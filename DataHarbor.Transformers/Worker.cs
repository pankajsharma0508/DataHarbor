namespace DataHarbor.Transformers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITransformationService _transformationService;

        public Worker(ILogger<Worker> logger, ITransformationService transformationService)
        {
            _logger = logger;
            _transformationService = transformationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                _transformationService.Transform();
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}
