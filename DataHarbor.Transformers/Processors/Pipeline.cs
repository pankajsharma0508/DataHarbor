using DataHarbor.Common.Process;

namespace DataHarbor.Transformers.Processors
{
    public interface IPipelineStep<T>
    {
        Task ProcessAsync(ProcessContext context);
    }

    public class Pipeline<T>
    {
        private readonly List<IPipelineStep<T>> _steps = [];

        public Pipeline<T> AddStep(IPipelineStep<T> step)
        {
            _steps.Add(step);
            return this;
        }

        public async Task<ProcessContext> ExecuteAsync(ProcessContext context)
        {
            foreach (var step in _steps)
            {
                await step.ProcessAsync(context);
            }
            return context;
        }
    }

}
