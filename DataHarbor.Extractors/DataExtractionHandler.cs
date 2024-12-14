using MassTransit;
using DataHarbor.Common.Messaging;

namespace DataHarbor.Extractors
{
    internal class DataExtractionHandler : IConsumer<DataExtractRequest>
    {
        public Task Consume(ConsumeContext<DataExtractRequest> context)
        {
            Console.WriteLine($"Received message: {context.Message.FilePath}");
            return Task.CompletedTask;
        }
    }
}
