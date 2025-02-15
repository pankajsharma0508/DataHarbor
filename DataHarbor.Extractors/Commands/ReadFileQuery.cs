using DataHarbor.Common.Configuration;
using MediatR;
using System.Dynamic;

namespace DataHarbor.Extractors.Commands
{
    public record ReadFileQuery(FilesConfigurations fileConfigurations, string FilePath) : IRequest<List<ExpandoObject>>;
}
