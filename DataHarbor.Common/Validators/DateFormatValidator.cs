using FluentValidation;
using FluentValidation.Results;

namespace DataHarbor.Common.Validators
{
    public class DateFormatValidator : AbstractValidator<DateRequest>
    {
        private string DateFormat { get; set; }

        public DateFormatValidator(string dateFormat, bool canBeEmpty = false)
        {
            DateFormat = dateFormat;
            RuleFor(x => x.DateValue).NotEmpty()
                .WithMessage("Date is Required.")
                .Must(IsInValidFormat)
                .WithMessage($"Invalid date format. Please {dateFormat}");
        }

        private bool IsInValidFormat(string date)
        {
            return DateTime.TryParseExact(date, DateFormat,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out _);
        }
    }

    public class DateRequest(string dateValue)
    {
        public string DateValue { get; set; } = dateValue;
    }
}
