using System.Globalization;
using System.Windows.Controls;

namespace GoalTracker.Validations
{
    internal class StringTextBoxValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value as string;

            if (string.IsNullOrEmpty(text))
            {
                return new ValidationResult(false, "String cannot be empty.");
            }

            if (text.Length > 40)
            {
                return new ValidationResult(false, "String must be less than 40 characters long.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
