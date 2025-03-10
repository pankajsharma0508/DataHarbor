using DataHarbor.Common.Models;

namespace DataHarbor.WebAPI.Models
{
    public class Declaration
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public ProcessStatus Status { get; set; }

        public DateTime RecieveDate { get; set; }

        public List<Dictionary<string, string>> RawData { get; set; } = [];

        public List<Dictionary<string, string>> Transactions { get; set; } = [];
    }
}
