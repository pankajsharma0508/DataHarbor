using DataHarbor.Common.Messaging;
using DataHarbor.Common.Repository;
using System.Data;
using System.Dynamic;

namespace DataHarbor.Common.Models
{
    public class ProcessRequest : BaseDocument
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public ProcessStatus Status { get; set; }

        public DateTime RecieveDate { get; set; }

        public DataTable Data { get; set; }
    }

    public enum ProcessStatus
    {
        New,
        Extraction,
        Transformation,
        Loaded,
        Completed
    }
}
