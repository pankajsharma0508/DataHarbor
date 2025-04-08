﻿using DataHarbor.Common.Configuration;
using DataHarbor.Common.Constants;
using DataHarbor.Common.Models;
using DataHarbor.Common.Process;
using System.Data;

namespace DataHarbor.Transformers.Processors
{
    /// <summary>
    /// This processor will map the input data to the standard layout format.
    /// </summary>
    public class LayoutProcessor : IPipelineStep<ProcessRequest>
    {
        public Task ProcessAsync(ProcessContext context)
        {
            Console.WriteLine("Layout Processor");
            Process(context);
            return Task.CompletedTask;
        }


        private void Process(ProcessContext context)
        {
            var mappings = context.Configuration.LayoutMappings;
            var table = initializeTable(mappings);
            var rawData = context.Declaration.RawData;
            foreach (DataRow row in rawData.Rows)
            {
                var newRow  = table.NewRow();
                foreach (DataColumn column in rawData.Columns)
                {
                    var mapping = mappings.FirstOrDefault(x => x.SourceColumn == column?.ColumnName);
                    if (mapping != null)
                    {
                        newRow[mapping.FieldName] = row[mapping.SourceColumn];
                    }
                }
                table.Rows.Add(newRow);
                table.AcceptChanges();
            }
            context.Declaration.Transactions = table;
        }

        private DataTable initializeTable(List<LayoutMapping> mappings)
        {
            var data = new DataTable();
            foreach (var mapping in mappings.DistinctBy(x => x.FieldName))
            {
                data.Columns.Add(mapping.FieldName);
            }
            data.AcceptChanges();
            return data;
        }
    }
}
