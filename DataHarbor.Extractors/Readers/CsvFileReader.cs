using DataHarbor.Extractors.Constants;
using System.Dynamic;

namespace DataHarbor.Extractors.Readers
{
    public class CsvFileReader : IFileReader
    {
        public bool CanRead(string fileExtension) => fileExtension.Equals(FileExtensions.CSV);

        public List<ExpandoObject> ReadFile(string filePath)
        {
            var result = new List<ExpandoObject>();
            var lines = File.ReadAllLines(filePath);
            if (lines?.Count() > 0)
            {
                foreach (var line in lines)
                {
                    var values = line.Split(',');
                    var entry = new ExpandoObject();
                    int columnIndex = 0;
                    foreach (var value in values)
                    {
                        columnIndex++;
                        (entry as IDictionary<string, object>)[$"column{columnIndex}"] = value;
                    }
                    result.Add(entry);
                }
            }
            return result;
        }
    }
}
