using System.ComponentModel.DataAnnotations;

namespace TodoApp.API.Validators;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class RoleValidatorAttribute : ValidationAttribute
{
       protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return new ValidationResult("Role is required.");
        }

        var role = value.ToString();

        if (string.IsNullOrEmpty(role))
        {
            return new ValidationResult("Role is required.");
        }

        if (role != "Admin" && role != "User")
        {
            return new ValidationResult("Role must be either Admin or User.");
        }

        return ValidationResult.Success!;
    }
}

