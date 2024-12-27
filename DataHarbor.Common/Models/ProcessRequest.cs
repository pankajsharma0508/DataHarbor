using System.Dynamic;

namespace DataHarbor.Common.Models
{
    public class ProcessRequest
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime RecieveDate { get; set; }

        public List<ExpandoObject> Entries { get; set; }

        public ProcessRequest() => Entries = [];
    }
}
