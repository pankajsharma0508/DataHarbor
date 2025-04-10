using DataHarbor.Common.Constants;
using DataHarbor.Common.Models;
using DataHarbor.Loaders.Commands;
using DataHarbor.Repository;
using MediatR;
using System.Data;
using System.Linq;
using System.Transactions;

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
                var accountTable = book.Transactions.Copy();

                // Find new transaction ids.
                var existingIds = transactions.AsEnumerable()
                    .Select(x => x.Field<string>(MetadataHeader.GUID)).Distinct();

                // remove existing trasaction ids if reprocessed.
                var uniqueRows = accountTable.AsEnumerable()
                    .Where(row => !existingIds.Contains(row.Field<string>(MetadataHeader.GUID)));

                if (uniqueRows.Count() > 0)
                {
                    DataTable updated = uniqueRows.CopyToDataTable();
                    updated.Merge(transactions);
                    updated.AcceptChanges();
                    book.Transactions = updated;
                }
                else
                {
                    book.Transactions = transactions;
                }
                await _resultRepository.Update(book);
            }

            var declaration = await _declarationRepository.GetByID(command.Request.Id);
            declaration.Status = ProcessStatus.Dispatched;
            return await _declarationRepository.Update(declaration);
        }
    }
}
