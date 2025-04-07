using DataHarbor.Common.Models;
using DataHarbor.Loaders.Commands;
using DataHarbor.Repository;
using MediatR;

namespace DataHarbor.Loaders.Handlers
{
    public class UpdateAccountingBookHandler : IRequestHandler<UpdateAccountingBookCommand, ProcessRequest>
    {
        private readonly IRepository<ProcessResult> _resultRepository;
        private readonly IRepository<ProcessRequest> _declarationRepository;

        public UpdateAccountingBookHandler(IRepository<ProcessResult> repository, IRepository<ProcessRequest> declarationRepository)
        {
            _resultRepository = repository;
            _declarationRepository = declarationRepository;
        }

        public async Task<ProcessRequest> Handle(UpdateAccountingBookCommand command, CancellationToken cancellationToken)
        {
            var transactions = command.Request.Transactions;
            var accountName = command.Request.Name;

            var book = await _resultRepository.FirstOrDefault(x => x.Name == accountName);
            if (book == null)
            {
                book = new ProcessResult();
                book.Name = accountName;
                book.UniqueId = Guid.NewGuid();
                book.Transactions = transactions;
                await _resultRepository.Add(book);
            }
            else
            {
                book.Transactions.Merge(transactions);
                book.Transactions.AcceptChanges();
                await _resultRepository.Update(book);
            }

            var declaration = await _declarationRepository.GetByID(command.Request.Id);
            declaration.Status = ProcessStatus.Dispatched;
            return await _declarationRepository.Update(declaration);
        }
    }
}
