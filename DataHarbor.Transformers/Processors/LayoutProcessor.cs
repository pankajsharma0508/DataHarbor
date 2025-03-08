using DataHarbor.Common.Models;
using DataHarbor.Common.Process;

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

            return Task.CompletedTask;
        }
    }
}
