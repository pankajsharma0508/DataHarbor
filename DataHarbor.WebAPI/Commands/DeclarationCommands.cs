using DataHarbor.Common.Messaging;
using DataHarbor.WebAPI.Models;
using MediatR;

namespace DataHarbor.WebAPI.Commands
{

    public record CreateDeclarationCommand(Declaration declaration) : IRequest<Declaration>;

    public record UpdateDeclarationCommand(Declaration declaration) : IRequest<Declaration>;

    public record DeleteDeclarationCommand(string id) : IRequest;

}
