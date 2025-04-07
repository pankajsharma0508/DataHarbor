namespace DataHarbor.Common.Messaging
{
    public class ProcessMessage
    {
        public Guid DeclarationId { get; set; }
        public ProcessMessageStatus Status { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime RecievedOn { get; set; }
    }

    public enum ProcessMessageStatus
    {
        Anchored,
        Adrifted,
        Docked
    }
}
