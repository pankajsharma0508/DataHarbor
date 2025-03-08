using DataHarbor.Common.Models;
using DataHarbor.WebAPI.Models;
using MediatR;

namespace DataHarbor.WebAPI.Commands
{

    public record CreateRequestCommand(Declaration request) : IRequest;

    public record UpdateRequestCommand(Declaration request) : IRequest;

    public record DeleteRequestCommand(string id) : IRequest;

}
