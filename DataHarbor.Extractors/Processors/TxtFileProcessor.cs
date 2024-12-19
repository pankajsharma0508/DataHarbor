using DataHarbor.Extractors.Constants;

namespace DataHarbor.Extractors.Processors
{
    public class TxtFileProcessor : IFileProcessor
    {
        public bool CanProcess(string fileExtension) => fileExtension.Equals(FileExtensions.TXT);

        public void ProcessFile(string filePath)
        {
        }
    }
}
