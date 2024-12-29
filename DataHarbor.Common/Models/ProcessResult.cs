using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Common.Models
{
    public class ProcessResult
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime RecieveDate { get; set; }

        public List<Transaction> Entries { get; set; }

        public ProcessResult() => Entries = [];
    }
}
