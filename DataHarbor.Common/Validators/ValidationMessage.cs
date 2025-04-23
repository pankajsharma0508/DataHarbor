using FluentValidation;

namespace DataHarbor.Common.Validators
{
    public class ValidationMessage
    {
        public Guid Id { get; set; }
        public int RecordId { get; set; } 
        public string Summary { get; set; }
        public string Message { get; set; }
        public Severity Severity { get; set; }
    }
}
