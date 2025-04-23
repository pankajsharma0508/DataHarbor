using DataHarbor.Common.Process;
using DataHarbor.Common.Repository;
using System.Data;
using System.Text.Json.Serialization;

namespace DataHarbor.Common.Models
{
    public class ProcessRequest : IDocument
    {
        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string FilePath { get; set; }

        public string Description { get; set; }

        public ProcessStatus Status { get; set; }

        public DateTime RecieveDate { get; set; }

        public DataTable RawData { get; set; }

        public DataTable Transactions { get; set; }

        public List<ProcessingLogEntry> ProcessingLogs { get; set; } = [];
        public string Id { get => UniqueId.ToString(); set => UniqueId = new Guid(value); }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProcessStatus
    {
        Anchored,
        Docked,
        Adrifted,
        Dispatched,
        Error
    }
}
