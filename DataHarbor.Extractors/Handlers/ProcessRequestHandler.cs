﻿using DataHarbor.Common.Constants;
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
            context.Declaration.Status = ProcessStatus.Docked;
            context.Declaration.RawData = data;
            context.Declaration.ProcessingLogs = context.ProcessingLogs;
            repository.Update(context.Declaration);

            return Task.CompletedTask;
        }
    }
}
