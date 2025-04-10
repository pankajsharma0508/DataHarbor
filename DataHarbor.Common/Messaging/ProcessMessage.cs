using System.Text.Json.Serialization;

namespace DataHarbor.Common.Messaging
{
    /// <summary>
    /// Process Message
    /// </summary>
    public class ProcessMessage
    {
        public Guid DeclarationId { get; set; }

        public ProcessMessageStatus Status { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProcessMessageStatus
    {
        Anchored,
        Docked,
        Adrifted,
    }
}
