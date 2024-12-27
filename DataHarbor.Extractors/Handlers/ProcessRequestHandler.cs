using DataHarbor.Extractors.Commands;
using DataHarbor.Repository;
using MediatR;

namespace DataHarbor.Extractors.Handlers
{
    public class ProcessRequestHandler : IRequestHandler<ProcessRequestCommand, bool>
    {
        public Task<bool> Handle(ProcessRequestCommand command, CancellationToken cancellationToken)
        {
            var repository = new RequestRepository();
            return repository.Add(command.ProcessRequest);
        }
    }
}
