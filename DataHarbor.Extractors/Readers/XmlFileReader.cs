using DataHarbor.Extractors.Constants;
using System.Dynamic;

namespace DataHarbor.Extractors.Readers
{
    internal class XmlFileReader : IFileReader
    {
        public bool CanRead(string fileExtension) => fileExtension.Equals(FileExtensions.XML);

        public List<ExpandoObject> ReadFile(string filePath)
        {
            return [];
        }
    }
}
