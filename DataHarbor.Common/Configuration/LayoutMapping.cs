using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Common.Configuration
{
    public class LayoutMapping
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; }
        public string SourceColumn { get; set; }
        public bool ShowInOutput { get; set; }
        public int FieldOrder { get; set; }
        public string FieldType { get; set; }
        public string FormatPattern { get; set; }
        public bool IsUnique { get; set; }
        public int? Length { get; set; }
    }

    public class CategoryLayoutMapping
    {
        public CategoryLayoutMapping()
        {
            LayoutMappings = [];
        }
        public string Category { get; set; }

        public List<LayoutMapping> LayoutMappings { get; set; }
    }
}
