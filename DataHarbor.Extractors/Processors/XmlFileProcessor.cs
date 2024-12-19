using DataHarbor.Extractors.Constants;

namespace DataHarbor.Extractors.Processors
{
    public class XmlFileProcessor : IFileProcessor
    {
        public bool CanProcess(string fileExtension) => fileExtension.Equals(FileExtensions.XML);

        public void ProcessFile(string filePath)
        {
        }
    }
}
