using DataHarbor.Common.Repository;
using System.Data;

namespace DataHarbor.Common.Models
{
    public class ProcessResult : IDocument
    {
        public string Id { get => UniqueId.ToString(); set => UniqueId = new Guid(value); }

        public Guid UniqueId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime RecieveDate { get; set; }

        public DataTable Transactions { get; set; }
    }
}
