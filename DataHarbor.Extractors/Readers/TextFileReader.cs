using DataHarbor.Common.Configuration;
using DataHarbor.Extractors.Constants;
using System.Dynamic;

namespace DataHarbor.Extractors.Readers
{
    public class TextFileReader : IFileReader
    {
        private readonly string[] supportedFileTypes = [FileExtensions.CSV, FileExtensions.TXT, FileExtensions.DAT];
        public bool CanRead(string fileExtension) => supportedFileTypes.Contains(fileExtension);

        public List<ExpandoObject> ReadFile(FilesConfigurations configuration, string filePath)
        {
            var result = new List<ExpandoObject>();
            var lines = GetFilteredLine(configuration, filePath);
            if (lines?.Count() > 0)
            {
                var columnNames = GetColumnNames(configuration, lines);
                var linesWithoutHeaders = configuration.FirstRowHasHeaders ? lines.Skip(1) : lines;
                foreach (var line in linesWithoutHeaders)
                {
                    var values = line.Split(configuration.ColumnSeparator);
                    var entry = new ExpandoObject();
                    int columnIndex = 0;
                    foreach (var value in values)
                    {
                        var columnName = columnNames.ElementAt(columnIndex);
                        (entry as IDictionary<string, object>)[columnName] = value;
                        columnIndex++;
                    }
                    result.Add(entry);
                }
            }
            return result;
        }

        private List<string> GetFilteredLine(FilesConfigurations configuration, string filePath)
        {
            var fileContent = File.ReadAllText(filePath);

            // check for empty line seperator;
            // check for empty column seperator;

            var lines = fileContent.Split(configuration.LineSeparator).ToList();
            if (configuration.HeaderRowsToIgnore > 0)
            {
                lines = lines.Skip(configuration.HeaderRowsToIgnore).ToList();
            }
            if (configuration.FooterRowsToIgnore > 0 && lines.Count() > configuration.FooterRowsToIgnore)
            {
                lines = lines.SkipLast(configuration.FooterRowsToIgnore).ToList();
            }
            return lines;
        }

        private List<string> GetColumnNames(FilesConfigurations configuration, List<string> lines)
        {
            var columnNames = new List<string>();
            var firstLine = lines.First();
            if (configuration.FirstRowHasHeaders)
            {
                columnNames = firstLine.Split(configuration.ColumnSeparator).ToList();
            }
            else
            {
                columnNames = firstLine.Split(configuration.ColumnSeparator)
                    .Select((x, index) => $"column{index}").ToList();
            }
            return columnNames;
        }
    }
}
