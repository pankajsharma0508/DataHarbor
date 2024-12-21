using DataHarbor.Extractors.Processors;
using DataHarbor.Extractors.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Extractors.Handlers
{
    public class FileReaderResolver
    {
        private readonly IEnumerable<IFileReader> _fileReader;

        public FileReaderResolver(IEnumerable<IFileReader> fileReader)
        {
            _fileReader = fileReader;
        }

        public IFileReader GetProcessor(string fileExtension)
        {
            var processor = _fileReader.FirstOrDefault(p => p.CanRead(fileExtension));
            if (processor == null)
            {
                throw new NotSupportedException($"No processor found for file type: {fileExtension}");
            }
            return processor;
        }

    }
}
