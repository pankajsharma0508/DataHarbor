using DataHarbor.Extractors.Processors;

namespace DataHarbor.Extractors
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, FileProcessorResolver resolver)
        {
            _logger = logger;
            Resolver = resolver;
        }

        public FileProcessorResolver Resolver { get; }

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
                    var lines = await File.ReadAllLinesAsync(fileInfo.FullName, stoppingToken)
                        .ConfigureAwait(false);
                    if (lines != null)
                    {
                        var processor = Resolver.GetProcessor(fileInfo.Extension);
                        processor.ProcessFile(fileInfo.FullName);
                    }
                }

                //await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
