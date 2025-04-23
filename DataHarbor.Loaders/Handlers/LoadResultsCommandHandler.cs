using DataHarbor.Common.Models;
using DataHarbor.Common.Process;
using DataHarbor.Loaders.Commands;
using DataHarbor.Loaders.Services;
using MediatR;

namespace DataHarbor.Loaders.Handlers
{
    public class LoadResultsCommandHandler : IRequestHandler<LoadResultsCommand, ProcessContext>
    {
        private readonly IDbaseService<Transaction> _dbaseService;

        public LoadResultsCommandHandler(IDbaseService<Transaction> dbaseService)
        {
            _dbaseService = dbaseService;
        }

        public Task<ProcessContext> Handle(LoadResultsCommand command, CancellationToken cancellationToken)
        {
            var configuration = command.Context.Configuration;
            var filePath = configuration?.MailboxFilePath;
            var fileName = configuration?.MailboxFileName;

            // fall back to some default location
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(fileName))
            {

            }
            var declaration = command.Context.Declaration;
            _dbaseService.CreateOrUpdateFile($"{filePath}\\{fileName}", declaration.Transactions);
            return Task.FromResult(command.Context);
        }
    }
}
