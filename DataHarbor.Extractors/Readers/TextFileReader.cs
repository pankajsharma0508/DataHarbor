﻿using DataHarbor.Common.Configuration;
using DataHarbor.Common.Constants;
using DataHarbor.Common.Helpers;
using DataHarbor.Common.Models;
using DataHarbor.Extractors.Constants;
using System.Data;
using System.Dynamic;

namespace DataHarbor.Extractors.Readers
{
    public class TextFileReader : IFileReader
    {
        private readonly string[] supportedFileTypes = [FileExtensions.CSV, FileExtensions.TXT, FileExtensions.DAT];
        public bool CanRead(string fileExtension) => supportedFileTypes.Contains(fileExtension);

        public DataTable ReadFile(FilesConfigurations configuration, ProcessRequest request)
        {
            var table = new DataTable();
            var lines = GetFilteredLine(configuration, request);
            if (lines?.Count() > 0)
            {
                var columnNames = GetColumnNames(configuration, lines);
                table.AssureColumns(columnNames);
                table.AssureColumn(MetadataHeader.RecordId);

                var linesWithoutHeaders = configuration.FirstRowHasHeaders ? lines.Skip(1) : lines;
                var rowIndex = 0;
                foreach (var line in linesWithoutHeaders)
                {
                    var values = line.Split(configuration.ColumnSeparator);
                    var row = table.NewRow();
                    row[MetadataHeader.RecordId] = ++rowIndex;

                    int columnIndex = 0;
                    foreach (var value in values)
                    {
                        var columnName = columnNames.ElementAt(columnIndex);
                        row[columnName] = value;
                        columnIndex++;
                    }
                    table.Rows.Add(row);
                    table.AcceptChanges();
                }
            }
            return table;
        }

        private List<string> GetFilteredLine(FilesConfigurations configuration, ProcessRequest request)
        {
            var allLines = new List<string>();
            foreach (var attachment in request.Attachments)
            {
                using (var reader = new StreamReader(attachment.FileStream))
                {
                    string fileText = reader.ReadToEnd();
                    var lines = fileText.Split(configuration.LineSeparator, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (configuration.HeaderRowsToIgnore > 0)
                    {
                        lines = lines.Skip(configuration.HeaderRowsToIgnore).ToList();
                    }
                    if (configuration.FooterRowsToIgnore > 0 && lines.Count() > configuration.FooterRowsToIgnore)
                    {
                        lines = lines.SkipLast(configuration.FooterRowsToIgnore).ToList();
                    }
                    allLines.AddRange(lines);
                }
            }
            return allLines;
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
                    .Select((x, index) => $"Column{index + 1}").ToList();
            }
            return columnNames;
        }

    }
}
