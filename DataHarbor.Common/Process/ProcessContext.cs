using DataHarbor.Common.Messaging;
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
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string FilePath { get; set; }

        private List<ProcessContextData> ProcessContextParameters = [];
        public List<ValidationMessage> ValidationMessages { get; set; } = [];

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

        public void LogMessage(ValidationMessage message) => this.ValidationMessages.Add(message);

        public void LogMessage(string summary, string message = null, Severity severity = Severity.Info, int recordId = 0)
        {
            this.ValidationMessages.Add(new ValidationMessage
            {
                Id = Guid.NewGuid(),
                Summary = summary ?? message,
                Message = message,
                RecordId = recordId,
                Severity = Severity.Info
            });
        }
    }

}
