using DataHarbor.Common.Models;
using DataHarbor.Loaders.Queries;
using DataHarbor.Repository;
using MediatR;

namespace DataHarbor.Loaders.Handlers
{
    public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, ProcessRequest>
    {
        private readonly IRepository<ProcessRequest> repository;

        public GetTransactionsHandler(IRepository<ProcessRequest> repository)
        {
            this.repository = repository;
        }

        public Task<ProcessRequest> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            return repository.GetByID(request.id.ToString());
        }
    }
}
