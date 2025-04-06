using AutoMapper;
using DataHarbor.Common.Configuration;
using DataHarbor.Common.Models;
using DataHarbor.Repository;
using DataHarbor.WebAPI.Models;
using DataHarbor.WebAPI.Query;
using MediatR;

namespace DataHarbor.WebAPI.Handlers
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<Account>>
    {
        private readonly IRepository<ProcessResult> repository;
        private readonly IMapper _mapper;

        public GetAccountsQueryHandler(IRepository<ProcessResult> repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Account>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await repository.GetAll();
            return _mapper.Map<List<Account>>(accounts);
        }
    }

    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, Account>
    {
        private readonly IRepository<ProcessResult> repository;
        private readonly IMapper _mapper;

        public GetAccountQueryHandler(IRepository<ProcessResult> repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<Account> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var accounts = await repository.FirstOrDefault(x => x.Id == request.id);
            return _mapper.Map<Account>(accounts);
        }
    }
}
