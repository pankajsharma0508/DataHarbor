using AutoMapper;
using DataHarbor.Common.Models;
using DataHarbor.Repository;
using DataHarbor.WebAPI.Models;
using DataHarbor.WebAPI.Query;
using MediatR;

namespace DataHarbor.WebAPI.Handlers
{
    public class GetProcessRequestQueryHandler : IRequestHandler<GetProcessRequestQuery, Declaration>
    {
        private readonly IRepository<ProcessRequest> repository;
        private readonly IMapper _mapper;

        public GetProcessRequestQueryHandler(IRepository<ProcessRequest> repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public Task<Declaration> Handle(GetProcessRequestQuery query, CancellationToken cancellationToken)
        {
           var request = this.repository.GetByID(query.id).Result;
           return Task.FromResult(_mapper.Map<Declaration>(request));
        }
    }

    public class GetProcessRequestsQueryHandler : IRequestHandler<GetProcessRequestsQuery, List<Declaration>>
    {
        private readonly IRepository<ProcessRequest> repository;
        private readonly IMapper _mapper;

        public GetProcessRequestsQueryHandler(IRepository<ProcessRequest> repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public Task<List<Declaration>> Handle(GetProcessRequestsQuery request, CancellationToken cancellationToken)
        {
            var declarations = repository.GetAll().Result
                .Select(x=>_mapper.Map<Declaration>(x))
                .ToList();

            return Task.FromResult(declarations);
        }
    }
}
