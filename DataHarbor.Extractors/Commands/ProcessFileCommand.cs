using MediatR;

namespace DataHarbor.Extractors.Commands
{
    public record ProcessFileCommand(string FilePath, string FileExtension) : IRequest;
}
