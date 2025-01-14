using DataHarbor.Common.Models;
using DataHarbor.Repository;
using DataHarbor.WebAPI.Query;
using MediatR;

namespace DataHarbor.WebAPI.Handlers
{
    public class GetProcessRequestsQueryHandler : IRequestHandler<GetProcessRequestsQuery, List<ProcessRequest>>
    {
        private readonly IRepository<ProcessRequest> repository;

        public GetProcessRequestsQueryHandler(IRepository<ProcessRequest> repository)
        {
            this.repository = repository;
        }

        public Task<List<ProcessRequest>> Handle(GetProcessRequestsQuery request, CancellationToken cancellationToken)
        {
            return repository.GetAll();
        }
    }
}
