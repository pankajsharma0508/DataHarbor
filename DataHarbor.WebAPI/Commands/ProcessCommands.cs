using DataHarbor.Common.Messaging;
using MediatR;

namespace DataHarbor.WebAPI.Commands
{
    public record DataProcessMessage(ProcessMessage message) : IRequest;
}

