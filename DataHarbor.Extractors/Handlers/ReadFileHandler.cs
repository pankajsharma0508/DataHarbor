using DataHarbor.Extractors.Commands;
using MediatR;
using DataHarbor.Common.Configuration;
using DataHarbor.Common.Process;
using DataHarbor.Common.Constants;
using DataHarbor.Common.Models;

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
            var configuration = request.context.Configuration;
            if (configuration == null || request.context.ContainsCriticalError())
            {
                return Task.FromResult(request.context);
            }

            var declaration = request.context.Declaration;
            var fileConfiguration = configuration.OperatorFilesConfigurations;
            var processor = _resolver.GetProcessor(fileConfiguration.FileFormat);
            var result = processor.ReadFile(fileConfiguration, declaration?.FilePath);

            request.context.ProcessingResults.Add(ProcessingResultNames.LoadSourceData, result);

            request.context.LogMessage("File Loading", "File loaded successfully.",
               ProcessingLogConstants.Category_File_Loading, ProcessingSeverity.Info);

            return Task.FromResult(request.context);
        }
    }
}
