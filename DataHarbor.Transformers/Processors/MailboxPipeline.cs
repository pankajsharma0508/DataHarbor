using DataHarbor.Common.Models;

namespace DataHarbor.Transformers.Processors
{
    public static class MailboxPipeline
    {
        public static Pipeline<ProcessRequest> GetPipeline() => new Pipeline<ProcessRequest>()
             .AddStep(new DataFormatValidator())
             .AddStep(new LayoutProcessor());
    }
}
