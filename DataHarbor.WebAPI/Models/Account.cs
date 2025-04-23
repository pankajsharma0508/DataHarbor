using System.Data;

namespace DataHarbor.WebAPI.Models
{
    public class Account
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Dictionary<string, string>> Transactions { get; set; } = [];
    }
}
