using DataHarbor.Common.Models;
using DataHarbor.Repository;
using DataHarbor.WebAPI.Query;
using MediatR;

namespace DataHarbor.WebAPI.Handlers
{
    public class GetProcessRequestQueryHandler : IRequestHandler<GetProcessRequestQuery, ProcessRequest>
    {
        private readonly IRepository<ProcessRequest> repository;

        public GetProcessRequestQueryHandler(IRepository<ProcessRequest> repository)
        {
            this.repository = repository;
        }

        public Task<ProcessRequest> Handle(GetProcessRequestQuery request, CancellationToken cancellationToken)
        {
           return this.repository.GetByID(request.id);
        }
    }
}
