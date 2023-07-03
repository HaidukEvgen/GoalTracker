using System.Globalization;
using System.Windows.Controls;

namespace GoalTracker.Validations
{
    internal class IntegerTextBoxValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string input)
            {
                if (int.TryParse(input, out _))
                {
                    return ValidationResult.ValidResult;
                }
            }
            return new ValidationResult(false, "Please enter a valid integer number.");
        }
    }
}
