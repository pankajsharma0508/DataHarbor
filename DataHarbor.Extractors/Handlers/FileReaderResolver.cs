using DataHarbor.Extractors.Readers;

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
