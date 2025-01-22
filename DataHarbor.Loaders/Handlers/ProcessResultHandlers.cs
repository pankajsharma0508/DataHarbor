using DataHarbor.Common.Models;
using DataHarbor.Loaders.Queries;
using DataHarbor.Repository;
using MediatR;

namespace DataHarbor.Loaders.Handlers
{
    public class GetProcessResultHandler : IRequestHandler<GetProcessResultQuery, ProcessResult>
    {
        private readonly IRepository<ProcessResult> repository;

        public GetProcessResultHandler(IRepository<ProcessResult> repository)
        {
            this.repository = repository;
        }

        public Task<ProcessResult> Handle(GetProcessResultQuery request, CancellationToken cancellationToken)
        {
            return repository.GetByID(request.id);
        }
    }
}
