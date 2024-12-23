using MediatR;
using System.Dynamic;

namespace DataHarbor.Extractors.Commands
{
    public record ReadFileQuery(string FilePath, string FileExtension) : IRequest<List<ExpandoObject>>;
}
