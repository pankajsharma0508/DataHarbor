using DataHarbor.Extractors.Constants;
using System.Dynamic;

namespace DataHarbor.Extractors.Readers
{
    public class CsvFileReader : IFileReader
    {
        public bool CanRead(string fileExtension) => fileExtension.Equals(FileExtensions.CSV);

        public List<ExpandoObject> ReadFile(string filePath)
        {
            return [];
        }
    }
}
