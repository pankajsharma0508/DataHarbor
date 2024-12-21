using MediatR;

namespace DataHarbor.Extractors.Commands
{
    public record ReadFileCommand(string FilePath, string FileExtension) : IRequest;
}
