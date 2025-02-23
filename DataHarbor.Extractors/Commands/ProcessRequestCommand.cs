using DataHarbor.Common.Models;
using DataHarbor.Common.Process;
using MediatR;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Extractors.Commands
{
    public record ProcessRequestCommand(ProcessContext Context) : IRequest;
}
