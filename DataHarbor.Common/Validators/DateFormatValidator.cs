using FluentValidation;
using FluentValidation.Results;
using System.Data;

namespace DataHarbor.Common.Validators
{
    public class DateFormatValidator : AbstractValidator<DataRow>
    {
        private string DateFormat { get; set; }

        public DateFormatValidator(string fieldName, string dateFormat = "dd-MM-yyyy")
        {
            DateFormat = dateFormat;
            RuleFor(x => x.Field<string>(fieldName))
                .NotEmpty()
                .WithMessage($"{fieldName} is Required.")
                .Must(IsInValidFormat)
                .WithMessage($"{fieldName} is in Invalid date format.");
        }

        private bool IsInValidFormat(string date)
        {
            return DateTime.TryParseExact(date, DateFormat,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
