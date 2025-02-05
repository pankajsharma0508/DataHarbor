using DataHarbor.Common.Models;
using MediatR;

namespace DataHarbor.WebAPI.Commands
{

    public record CreateRequestCommand(ProcessRequest request) : IRequest;

    public record UpdateRequestCommand(ProcessRequest request) : IRequest;

    public record DeleteRequestCommand(string id) : IRequest;

}
