namespace DataHarbor.Common.Messaging
{
    public class ProcessMessage
    {
        public Guid UniqueId { get; set; }
        public ProcessMessageStatus Status { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public DateTime RecievedOn { get; set; }
    }

    public enum ProcessMessageStatus
    {
        Ingest,
        TransferDispath,
        LoadSignal
    }
}
