using DataHarbor.Common.Constants;
using DataHarbor.Common.Models;
using DataHarbor.Extractors.Commands;
using DataHarbor.Repository;
using MediatR;

namespace DataHarbor.Extractors.Handlers
{
    public class ProcessRequestHandler : IRequestHandler<ProcessRequestCommand>
    {
        private readonly IRepository<ProcessRequest> repository;

        public ProcessRequestHandler(IRepository<ProcessRequest> repository)
        {
            this.repository = repository;
        }

        public Task Handle(ProcessRequestCommand command, CancellationToken cancellationToken)
        {
            var context = command.Context;
            var data = context.ProcessingResults.FirstOrDefault(x => x.Key == ProcessingResultNames.LoadSourceData).Value;
            var request = new ProcessRequest
            {
                Id = context.Id.ToString(),
                UniqueId = context.Id,
                Name = context.Name,
                Description = context.Name,
                RawData = data,
                RecieveDate = DateTime.UtcNow
            };
            repository.Add(request);

            return Task.CompletedTask;
        }
    }
}
