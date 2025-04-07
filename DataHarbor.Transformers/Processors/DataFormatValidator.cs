using DataHarbor.Common.Configuration;
using DataHarbor.Common.Constants;
using DataHarbor.Common.Models;
using DataHarbor.Common.Process;
using DataHarbor.Common.Validators;
using FluentValidation.Results;
using System.Data;

namespace DataHarbor.Transformers.Processors
{
    public class DataFormatValidator : IPipelineStep<ProcessRequest>
    {
        public Task ProcessAsync(ProcessContext context)
        {
            var validationResults = new List<ValidationResult>();
            var configuration = context.GetProcessingParameter(ProcessingResultNames.Configuration) as ProcessingConfiguration;
            if (configuration != null)
            {
                var request = context.GetProcessingParameter(ProcessingResultNames.ProcessingRequest) as ProcessRequest;
                if (request != null && request.RawData != null)
                {
                    var rawData = request.RawData.AsEnumerable();
                    foreach (var row in rawData)
                    {
                        foreach (var mapping in configuration.LayoutMappings)
                        {
                            validationResults.AddRange(ValidateFieldValue(row, mapping));
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }

        private IEnumerable<ValidationResult> ValidateFieldValue(DataRow row, LayoutMapping mapping)
        {
            var results = new List<ValidationResult>();
            if (mapping.FieldType == FieldTypes.Date)
            {
                results.Add(new DateFormatValidator(mapping.SourceColumn).Validate(row));
            }
            return results.SkipWhile(x => x.IsValid);
        }
    }
}
