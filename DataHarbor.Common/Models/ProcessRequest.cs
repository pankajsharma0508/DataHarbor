using DataHarbor.Common.Repository;
using System.Data;

namespace DataHarbor.Common.Models
{
    public class ProcessRequest : IDocument
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public ProcessStatus Status { get; set; }

        public DateTime RecieveDate { get; set; }

        public DataTable RawData { get; set; }

        public DataTable Transactions { get; set; }
        public string Id { get => UniqueId.ToString(); set => UniqueId = new Guid(value); }
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
