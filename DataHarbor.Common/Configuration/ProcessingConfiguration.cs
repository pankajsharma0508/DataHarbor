using DataHarbor.Common.Repository;

namespace DataHarbor.Common.Configuration
{
    public class ProcessingConfiguration : BaseDocument
    {
        public ProcessingConfiguration()
        {
            OperatorFilesConfigurations = new FilesConfigurations();
            LayoutMappings = [];
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ModifiedOn { get; set; }

        public FilesConfigurations OperatorFilesConfigurations { get; set; }

        public List<CategoryLayoutMapping> LayoutMappings { get; set; }
    }
}
