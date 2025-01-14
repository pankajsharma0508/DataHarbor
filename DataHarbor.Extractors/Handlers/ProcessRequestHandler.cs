using DataHarbor.Common.Models;
using DataHarbor.Extractors.Commands;
using DataHarbor.Repository;
using MediatR;

namespace DataHarbor.Extractors.Handlers
{
    public class ProcessRequestHandler : IRequestHandler<ProcessRequestCommand, bool>
    {
        private readonly IRepository<ProcessRequest> repository;

        public ProcessRequestHandler(IRepository<ProcessRequest> repository)
        {
            this.repository = repository;
        }
        public Task<bool> Handle(ProcessRequestCommand command, CancellationToken cancellationToken)
        {
            return repository.Add(command.ProcessRequest);
        }
    }
}
