using DataHarbor.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Loaders.Queries
{
    public record GetProcessResultQuery(string id) : IRequest<ProcessResult>;
}
