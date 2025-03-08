using DataHarbor.Common.Constants;
using DataHarbor.Common.Models;
using DataHarbor.Common.Process;

namespace DataHarbor.Transformers.Processors
{
    public class DataFormatValidator : IPipelineStep<ProcessRequest>
    {
        public Task ProcessAsync(ProcessContext context)
        {
            var request = context.GetProcessingParameter(ProcessingResultNames.RawData) as ProcessRequest;
            if (request != null)
            {

            }
            return Task.CompletedTask;
        }
    }
}
