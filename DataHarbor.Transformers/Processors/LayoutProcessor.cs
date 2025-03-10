using DataHarbor.Common.Configuration;
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

            var configuration = context.GetProcessingParameter(ProcessingResultNames.Configuration) as ProcessingConfiguration;
            var processRequest = context.GetProcessingParameter(ProcessingResultNames.RawData) as ProcessRequest;

            Process(configuration, processRequest);
            context.AddProcessingParameter(ProcessingResultNames.ProcessedResult, processRequest);

            return Task.CompletedTask;
        }


        private void Process(ProcessingConfiguration configuration, ProcessRequest request)
        {
            var mappings = configuration.LayoutMappings.First().LayoutMappings;
            var table = initializeTable(mappings);
            foreach (DataRow row in request.RawData.Rows)
            {
                var newRow  = table.NewRow();
                foreach (DataColumn column in request.RawData.Columns)
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
            request.Transactions = table;
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
