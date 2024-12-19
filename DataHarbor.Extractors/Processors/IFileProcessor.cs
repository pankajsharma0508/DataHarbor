namespace DataHarbor.Extractors.Processors
{
    public interface IFileProcessor
    {
        void ProcessFile(string filePath);
        bool CanProcess(string fileExtension);
    }
}
