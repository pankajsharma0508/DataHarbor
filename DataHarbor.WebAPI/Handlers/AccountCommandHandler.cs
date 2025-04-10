using AutoMapper;
using DataHarbor.Common.Models;
using DataHarbor.Repository;
using DataHarbor.WebAPI.Models;
using DataHarbor.WebAPI.Query;
using MediatR;
using System.Collections.Generic;

namespace DataHarbor.WebAPI.Handlers
{
    public class AccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
    {
        private readonly IRepository<ProcessResult> _repository;
        private readonly IMapper _mapper;

        public AccountCommandHandler(IRepository<ProcessResult> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = _mapper.Map<ProcessResult>(request.account);
            var added = await _repository.Add(account);
            return _mapper.Map<Account>(added);
        }
    }
}
