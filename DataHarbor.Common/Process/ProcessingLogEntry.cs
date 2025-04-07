using DataHarbor.Common.Models;
using FluentValidation;

namespace DataHarbor.Common.Process
{
    public class ProcessingLogEntry
    {
        public string ProcessingStage { get; set; }
        public string Summary { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Category { get; set; }
        public ProcessingSeverity Severity { get; set; }
        public int RecordID { get; set; }
    }
}
