using DataHarbor.Common.Models;
using DataHarbor.Common.Repository;

namespace DataHarbor.Common.Configuration
{
    public class ProcessingConfiguration : IDocument
    {
        public ProcessingConfiguration()
        {
            OperatorFilesConfigurations = new FilesConfigurations();
            LayoutMappings = [];
        }

        public string Id { get => UniqueId.ToString(); set => UniqueId = new Guid(value); }

        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string MailboxFileName { get; set; }

        public string MailboxFilePath { get; set; }

        public FilesConfigurations OperatorFilesConfigurations { get; set; }

        public List<LayoutMapping> LayoutMappings { get; set; }
   
        public List<Attachment> Attachments { get; set; }
    }
}
