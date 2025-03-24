using DataHarbor.Common.Models;
using DataHarbor.Loaders.Commands;
using DataHarbor.Loaders.Services;
using MediatR;

namespace DataHarbor.Loaders.Handlers
{
    public class LoadResultsCommandHandler : IRequestHandler<LoadResultsCommand, bool>
    {
        private readonly IDbaseService<Transaction> _dbaseService;

        public LoadResultsCommandHandler(IDbaseService<Transaction> dbaseService)
        {
            _dbaseService = dbaseService;
        }

        public Task<bool> Handle(LoadResultsCommand command, CancellationToken cancellationToken)
        {
            var configuration = command.Configuration;
            var filePath = configuration.MailboxFilePath;
            var fileName = configuration.MailboxFileName;

            // fall back to some default location
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(fileName))
            {

            }

            var result = _dbaseService.CreateOrUpdateFile($"{filePath}\\{fileName}", command.Request.Transactions);
            return Task.FromResult(result);
        }
    }
}
