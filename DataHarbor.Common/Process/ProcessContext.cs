using DataHarbor.Common.Configuration;
using DataHarbor.Common.Messaging;
using DataHarbor.Common.Models;
using DataHarbor.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Common.Process
{
    public class ProcessContext
    {
        private List<ProcessContextData> ProcessContextParameters = [];
        public List<ProcessingLogEntry> ProcessingLogs { get; set; } = [];
        public Dictionary<string, DataTable> ProcessingResults { get; set; } = [];

        public object? GetProcessingParameter(string name)
        {
            var parameter = this.ProcessContextParameters.FirstOrDefault(x => x.Name == name);
            return parameter?.Value;
        }
        public void AddProcessingParameter(string name, object value)
        {
            this.ProcessContextParameters.Add(new ProcessContextData { Name = name, Value = value });
        }

        public ProcessingConfiguration? Configuration
        {
            get { return GetProcessingParameter(nameof(ProcessingConfiguration)) as ProcessingConfiguration; }
            set { AddProcessingParameter(nameof(ProcessingConfiguration), value); }
        }

        public ProcessRequest? Declaration
        {
            get { return GetProcessingParameter(nameof(ProcessRequest)) as ProcessRequest; }
            set { AddProcessingParameter(nameof(ProcessRequest), value); }
        }

        public bool ContainsCriticalError() => ProcessingLogs.Any(x => x.Severity == ProcessingSeverity.Critical);

        public void LogMessage(ProcessingLogEntry message) => this.ProcessingLogs.Add(message);

        public void LogMessage(
            string summary,
            string message,
            string category,
            ProcessingSeverity severity = ProcessingSeverity.Debug,
            int recordId = 0)
        {
            this.ProcessingLogs.Add(new ProcessingLogEntry
            {
                ProcessingStage = summary,
                Category = category,
                Message = message,
                Severity = severity,
                TimeStamp = DateTime.UtcNow,
                RecordID = recordId
            });
        }
    }

}
