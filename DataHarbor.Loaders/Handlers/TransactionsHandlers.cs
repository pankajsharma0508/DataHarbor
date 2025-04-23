using DataHarbor.Common.Models;
using DataHarbor.Loaders.Queries;
using DataHarbor.Repository;
using MediatR;

namespace DataHarbor.Loaders.Handlers
{
    public class GetDeclarationQueryHandler : IRequestHandler<GetDeclarationQuery, ProcessRequest>
    {
        private readonly IRepository<ProcessRequest> repository;

        public GetDeclarationQueryHandler(IRepository<ProcessRequest> repository)
        {
            this.repository = repository;
        }

        public Task<ProcessRequest> Handle(GetDeclarationQuery request, CancellationToken cancellationToken)
        {
            return repository.GetByID(request.id.ToString());
        }
    }
}
