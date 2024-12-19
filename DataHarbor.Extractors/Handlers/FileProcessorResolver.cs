using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHarbor.Extractors.Processors;

namespace DataHarbor.Extractors.Commands
{
    public class FileProcessorResolver
    {
        private readonly IEnumerable<IFileProcessor> _fileProcessors;

        public FileProcessorResolver(IEnumerable<IFileProcessor> fileProcessors)
        {
            _fileProcessors = fileProcessors;
        }

        public IFileProcessor GetProcessor(string fileExtension)
        {
            var processor = _fileProcessors.FirstOrDefault(p => p.CanProcess(fileExtension));
            if (processor == null)
            {
                throw new NotSupportedException($"No processor found for file type: {fileExtension}");
            }
            return processor;
        }
    }
}
