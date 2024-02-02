using System.ComponentModel.DataAnnotations;

namespace TodoApp.API.Validators;

public class TodoTypeValidatorAttribute : ValidationAttribute
{
    private readonly string[] validTypes = {"Holiday", "Work", "Shopping", "Other"};
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return new ValidationResult("Type is required.");
        }

        var type = value.ToString();
        if (!validTypes.Contains(type))
        {
            return new ValidationResult("Type must be one of the following: 'Holiday', 'Work', 'Shopping', 'Other'");
        }

        return ValidationResult.Success!;
    }
}

