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
            context.Declaration.Status = context.ContainsCriticalError() ? ProcessStatus.Error : ProcessStatus.Docked;
            context.Declaration.RawData = data;
            context.Declaration.ProcessingLogs = context.ProcessingLogs;
            repository.Update(context.Declaration);

            command.Context.LogMessage("Declaration Loaded",
                "Declaration added successfully.",
                 ProcessingLogConstants.Category_Declaration_Loading, 
                 ProcessingSeverity.Info);

            return Task.CompletedTask;
        }
    }
}
