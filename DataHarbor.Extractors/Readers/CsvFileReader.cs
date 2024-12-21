using DataHarbor.Extractors.Constants;

namespace DataHarbor.Extractors.Readers
{
    public class CsvFileReader : IFileReader
    {
        public bool CanRead(string fileExtension) => fileExtension.Equals(FileExtensions.CSV);

        public void ReadFile(string filePath)
        {
        }
    }
}
