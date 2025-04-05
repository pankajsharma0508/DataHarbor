using DataHarbor.Common.Models;
using DataHarbor.Loaders.Commands;
using DataHarbor.Repository;
using MediatR;

namespace DataHarbor.Loaders.Handlers
{
    public class UpdateAccountingBookHandler : IRequestHandler<UpdateAccountingBookCommand, bool>
    {
        private readonly IRepository<ProcessResult> repository;

        public UpdateAccountingBookHandler(IRepository<ProcessResult> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Handle(UpdateAccountingBookCommand command, CancellationToken cancellationToken)
        {
            var transactions = command.Request.Transactions;
            var accountName = command.Request.Name;

            var book = await repository.FirstOrDefault(x => x.Name == accountName);
            if (book == null)
            {
                book = new ProcessResult();
                book.Name = accountName;
                book.UniqueId = Guid.NewGuid();
                book.Transactions = transactions;
                await repository.Add(book);
            }
            else
            {
                book.Transactions.Merge(transactions);
                book.Transactions.AcceptChanges();
                await repository.Update(book);
            }
            return true;
        }
    }
}
