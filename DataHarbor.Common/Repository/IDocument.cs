
using DataHarbor.Common.Models;
using Newtonsoft.Json;

namespace DataHarbor.Common.Repository
{
    public interface IDocument
    {
        string Id { get; set; }

        [JsonIgnore]
        List<Attachment> Attachments { get; set; }
    }
}
