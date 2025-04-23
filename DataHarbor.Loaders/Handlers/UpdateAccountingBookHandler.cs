using DataHarbor.Common.Constants;
using DataHarbor.Common.Extensions;
using DataHarbor.Common.Models;
using DataHarbor.Common.Process;
using DataHarbor.Loaders.Commands;
using DataHarbor.Repository;
using MediatR;
using System.Data;
using System.Linq;
using System.Transactions;

namespace DataHarbor.Loaders.Handlers
{
    public class UpdateAccountingBookHandler : IRequestHandler<UpdateAccountingBookCommand, ProcessContext>
    {
        private readonly IRepository<ProcessResult> _resultRepository;
        private readonly IRepository<ProcessRequest> _declarationRepository;

        public UpdateAccountingBookHandler(IRepository<ProcessResult> repository, IRepository<ProcessRequest> declarationRepository)
        {
            _resultRepository = repository;
            _declarationRepository = declarationRepository;
        }

        public async Task<ProcessContext> Handle(UpdateAccountingBookCommand command, CancellationToken cancellationToken)
        {
            var declaration = command.Context.Declaration;
            var book = await _resultRepository.FirstOrDefault(x => x.Name == declaration.Name);
            if (book == null)
            {
                book = new ProcessResult();
                book.Name = declaration?.Name;
                book.UniqueId = Guid.NewGuid();
                book.Transactions = declaration.Transactions;
                await _resultRepository.Add(book);
            }
            else
            {
                book.Transactions = book.Transactions.EnsureUniqueEntries(declaration.Transactions, MetadataHeader.GUID);
                await _resultRepository.Update(book);
            }
           
            declaration.Status = ProcessStatus.Dispatched;
            await _declarationRepository.Update(declaration);

            return command.Context;
        }
    }
}
