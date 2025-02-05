using DataHarbor.Common.Repository;

namespace DataHarbor.Common.Models
{
    public class ProcessResult : BaseDocument
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime RecieveDate { get; set; }

        public List<Transaction> Entries { get; set; }

        public ProcessResult() => Entries = [];
    }
}
