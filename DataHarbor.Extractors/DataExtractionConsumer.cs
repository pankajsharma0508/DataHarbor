using DataHarbor.Common.Messaging;
using DataHarbor.Extractors.Commands;
using MassTransit;
using MediatR;

namespace DataHarbor.Extractors
{
    public class DataExtractionConsumer : IConsumer<ProcessMessage>
    {
        private readonly ILogger<DataExtractionConsumer> _logger;
        private readonly IMediator _mediator;

        public DataExtractionConsumer(ILogger<DataExtractionConsumer> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<ProcessMessage> context)
        {
            Console.WriteLine($"Received message: {context.Message.FilePath}");

            FileInfo fileInfo = new(context.Message.FilePath);
            if (fileInfo.Exists)
            {
                var uniqueId = Guid.NewGuid();
                var inputData = await _mediator.Send(new ReadFileQuery(fileInfo.FullName, fileInfo.Extension));
                var request = new Common.Models.ProcessRequest
                {
                    UniqueId = uniqueId,
                    Id = uniqueId.ToString(),
                    Name = fileInfo.Name,
                    Description = fileInfo.Name,
                    Entries = inputData,
                    RecieveDate = fileInfo.CreationTime
                };
                await _mediator.Send(new ProcessRequestCommand(request));
            }
        }
    }
}
