using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Validations
{
    public class NotEqual : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public NotEqual(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
            var comparisonValue = otherProperty?.GetValue(validationContext.ObjectInstance);

            if (Equals(value, comparisonValue))
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}