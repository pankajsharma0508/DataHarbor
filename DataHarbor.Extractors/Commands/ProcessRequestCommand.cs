using DataHarbor.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Extractors.Commands
{
    public record ProcessRequestCommand(ProcessRequest ProcessRequest) : IRequest<bool>;
}
