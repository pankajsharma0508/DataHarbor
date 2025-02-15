using DataHarbor.Common.Configuration;
using System.Dynamic;

namespace DataHarbor.Extractors.Readers
{
    public interface IFileReader
    {
        List<ExpandoObject> ReadFile(FilesConfigurations configuration, string filePath);

        bool CanRead(string fileExtension);
    }
}
