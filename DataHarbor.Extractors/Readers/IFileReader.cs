namespace DataHarbor.Extractors.Readers
{
    public interface IFileReader
    {
        void ReadFile(string filePath);
        bool CanRead(string fileExtension);
    }
}
