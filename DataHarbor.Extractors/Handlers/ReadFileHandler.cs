using DataHarbor.Extractors.Commands;
using MediatR;

namespace DataHarbor.Extractors.Handlers
{
    public class ReadFileHandler : IRequestHandler<ReadFileCommand>
    {
        private readonly FileReaderResolver _resolver;

        public ReadFileHandler(FileReaderResolver resolver)
        {
            _resolver = resolver;
        }

        public Task Handle(ReadFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var processor = _resolver.GetProcessor(request.FileExtension);
                processor.ReadFile(request.FilePath);
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Task.CompletedTask;
        }
    }
}
