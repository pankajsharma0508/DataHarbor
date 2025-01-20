using DataHarbor.Common.Repository;

namespace DataHarbor.Common.Models
{
    public class DataConfiguration(string name, string description) : BaseDocument
    {
        public string Name { get; set; } = name;

        public string Description { get; set; } = description;

        public DateTime ModifiedOn { get; set; }

        public List<LayoutMapping> LayoutMappings { get; set; } = new List<LayoutMapping>();
    }
}
