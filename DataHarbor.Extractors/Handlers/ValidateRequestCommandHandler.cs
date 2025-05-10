using DataHarbor.Common.Constants;
using DataHarbor.Common.Models;
using DataHarbor.Common.Process;
using DataHarbor.Extractors.Commands;
using MediatR;

namespace DataHarbor.Extractors.Handlers
{
    public class ValidateRequestCommandHandler : IRequestHandler<ValidateRequestCommand, ProcessContext>
    {
        public Task<ProcessContext> Handle(ValidateRequestCommand request, CancellationToken cancellationToken)
        {
            var configuration = request.Context.Configuration;

            // Validate if the configuration is not defined.
            if (configuration == null)
            {
                request.Context.LogMessage("Missing Configuration", "Unable to find appropriate configuration.",
                    ProcessingLogConstants.Category_Preload_Validation, ProcessingSeverity.Critical);
            }
            else
            {
                var declaration = request.Context.Declaration;
                if (declaration == null)
                {
                    request.Context.LogMessage("File Access", "Unable to find declaration.", ProcessingLogConstants.Category_Preload_Validation, ProcessingSeverity.Critical);
                }
                else
                {
                    // Validate if the file format defined is not matching the reported file type.
                    if (declaration?.Attachments.Count == 0)
                    {
                        request.Context.LogMessage("File Access", "Unable to access transaction file.",
                                ProcessingLogConstants.Category_Preload_Validation, ProcessingSeverity.Critical);
                    }

                    foreach (var attachment in declaration.Attachments)
                    {
                        if (!attachment.FileName.Contains(configuration.OperatorFilesConfigurations.FileFormat))
                        {
                            request.Context.LogMessage("File Format", "Unexpected file format.", ProcessingLogConstants.Category_Preload_Validation,
                                ProcessingSeverity.Critical);
                        }
                    }

                    if (!request.Context.ContainsCriticalError())
                    {
                        request.Context.LogMessage("File Validation", "Validation completed successfully.",
                           ProcessingLogConstants.Category_Preload_Validation, ProcessingSeverity.Info);
                    }
                }
            }


            return Task.FromResult(request.Context);
        }
    }
}
