using DataHarbor.Common.Messaging;
using DataHarbor.WebAPI.Models;
using MediatR;

namespace DataHarbor.WebAPI.Commands
{

    public record CreateRequestCommand(Anchored message) : IRequest;

    public record UpdateRequestCommand(Declaration request) : IRequest;

    public record DeleteRequestCommand(string id) : IRequest;

}
