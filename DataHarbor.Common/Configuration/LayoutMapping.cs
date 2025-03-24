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
}
