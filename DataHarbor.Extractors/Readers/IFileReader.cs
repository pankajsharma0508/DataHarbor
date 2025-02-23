using DataHarbor.Common.Configuration;
using System.Data;
using System.Dynamic;

namespace DataHarbor.Extractors.Readers
{
    public interface IFileReader
    {
        DataTable ReadFile(FilesConfigurations configuration, string filePath);

        bool CanRead(string fileExtension);
    }
}
