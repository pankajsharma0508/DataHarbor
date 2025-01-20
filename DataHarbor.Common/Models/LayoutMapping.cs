namespace DataHarbor.Common.Models
{
    public class LayoutMapping
    {
        public required string SourceName { get; set; }

        public FieldType FieldType { get; set; } = FieldType.String;

        public string? SourcePattern { get; set; } 

        public required string TargetName { get; set; }

        public string? TargetPattern { get; set; }
    }

    public enum FieldType
    {
        Date,
        String,
        Bool
    }
}
