using DataHarbor.Extractors.Commands;
using MediatR;
using System.Data;
using System.Dynamic;
using static MassTransit.ValidationResultExtensions;

namespace DataHarbor.Extractors.Handlers
{
    public class ReadFileHandler : IRequestHandler<ReadFileQuery, List<ExpandoObject>>
    {
        private readonly FileReaderResolver _resolver;

        public ReadFileHandler(FileReaderResolver resolver)
        {
            _resolver = resolver;
        }

        public Task<List<ExpandoObject>> Handle(ReadFileQuery request, CancellationToken cancellationToken)
        {
            var processor = _resolver.GetProcessor(request.FileExtension);
            var result = processor.ReadFile(request.FilePath);
            return Task.FromResult(result);
        }
    }
}
