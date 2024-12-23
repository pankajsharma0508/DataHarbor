using System.Dynamic;

namespace DataHarbor.Extractors.Readers
{
    public interface IFileReader
    {
        List<ExpandoObject> ReadFile(string filePath);
        bool CanRead(string fileExtension);
    }
}
