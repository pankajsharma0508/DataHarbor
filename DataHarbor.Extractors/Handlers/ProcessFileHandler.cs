using DataHarbor.Extractors.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Extractors.Handlers
{
    public class ProcessFileHandler : IRequestHandler<ProcessFileCommand>
    {
        private readonly FileProcessorResolver _resolver;

        public ProcessFileHandler(FileProcessorResolver resolver)
        {
            _resolver = resolver;
        }

        public Task Handle(ProcessFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var processor = _resolver.GetProcessor(request.FileExtension);
                processor.ProcessFile(request.FilePath);
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Task.CompletedTask;
        }
    }
}
