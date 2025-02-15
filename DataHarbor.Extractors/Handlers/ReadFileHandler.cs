using DataHarbor.Extractors.Commands;
using MediatR;
using System.Dynamic;
using DataHarbor.Common.Configuration;
using DataHarbor.Repository;

namespace DataHarbor.Extractors.Handlers
{
    public class ReadFileHandler : IRequestHandler<ReadFileQuery, List<ExpandoObject>>
    {
        private readonly FileReaderResolver _resolver;

        public ReadFileHandler(FileReaderResolver resolver, IRepository<ProcessingConfiguration> configurationRepository)
        {
            _resolver = resolver;
        }

        public Task<List<ExpandoObject>> Handle(ReadFileQuery request, CancellationToken cancellationToken)
        {
            var processor = _resolver.GetProcessor(request.fileConfigurations.FileFormat);
            var result = processor.ReadFile(request.fileConfigurations, request.FilePath);
            return Task.FromResult(result);
        }
    }
}
