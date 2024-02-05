using System.ComponentModel.DataAnnotations;

namespace TodoApp.API.Validators;

/// <summary>
/// Validation attribute to ensure a date is greater than or equal to today.
/// </summary>
public class GreaterOrEqualToTodayAttribute : ValidationAttribute
{
    /// <summary>
    /// Validates the input value to ensure it is a date greater than or equal to today.
    /// </summary>
    /// <param name="value">The input value to validate.</param>
    /// <param name="validationContext">The context information about the validation operation.</param>
    /// <returns>A ValidationResult object that encapsulates the information about the validation operation.</returns>
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return ValidationResult.Success!;

        var date = DateTime.Parse(value!.ToString());

        if (date < DateTime.Today)
            return new ValidationResult("Date must be greater than or equal to today.");

        return ValidationResult.Success!;
    }

}

