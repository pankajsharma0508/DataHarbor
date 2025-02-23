using DataHarbor.Extractors.Commands;
using MediatR;
using DataHarbor.Common.Configuration;
using DataHarbor.Common.Process;
using DataHarbor.Common.Constants;

namespace DataHarbor.Extractors.Handlers
{
    public class ReadFileHandler : IRequestHandler<ReadFileQuery, ProcessContext>
    {
        private readonly FileReaderResolver _resolver;

        public ReadFileHandler(FileReaderResolver resolver)
        {
            _resolver = resolver;
        }

        public Task<ProcessContext> Handle(ReadFileQuery request, CancellationToken cancellationToken)
        {
            var configuration = request.context
                .GetProcessingParameter(nameof(ProcessingConfiguration));

            if (configuration == null)
            {
                request.context.LogMessage("Unable to find configuration.");
                return Task.FromResult(request.context);
            }
            var fileConfiguration = ((ProcessingConfiguration)configuration).OperatorFilesConfigurations;
            var processor = _resolver.GetProcessor(fileConfiguration.FileFormat);
            var result = processor.ReadFile(fileConfiguration, request.context.FilePath);

            request.context.ProcessingResults.Add(ProcessingResultNames.LoadSourceData, result);

            return Task.FromResult(request.context);
        }
    }
}
