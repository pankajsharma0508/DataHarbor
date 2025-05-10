using DataHarbor.Common.Configuration;
using DataHarbor.Common.Models;
using System.Data;
using System.Dynamic;

namespace DataHarbor.Extractors.Readers
{
    public interface IFileReader
    {
        DataTable ReadFile(FilesConfigurations configuration, ProcessRequest request);

        bool CanRead(string fileExtension);
    }
}
