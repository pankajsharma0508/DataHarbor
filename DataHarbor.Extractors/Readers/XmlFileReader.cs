using DataHarbor.Extractors.Constants;

namespace DataHarbor.Extractors.Readers
{
    internal class XmlFileReader : IFileReader
    {
        public bool CanRead(string fileExtension) => fileExtension.Equals(FileExtensions.XML);

        public void ReadFile(string filePath)
        {
        }
    }
}
