using DataHarbor.Extractors.Constants;

namespace DataHarbor.Extractors.Readers
{
    internal class DatFileReader : IFileReader
    {
        public bool CanRead(string fileExtension) => fileExtension.Equals(FileExtensions.DAT);

        public void ReadFile(string filePath)
        {
        }
    }
}
