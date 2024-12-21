using DataHarbor.Extractors.Constants;

namespace DataHarbor.Extractors.Readers
{
    public class TxtFileReader : IFileReader
    {
        public bool CanRead(string fileExtension) => fileExtension.Equals(FileExtensions.TXT);
      
        public void ReadFile(string filePath)
        {
        }
    }
}
