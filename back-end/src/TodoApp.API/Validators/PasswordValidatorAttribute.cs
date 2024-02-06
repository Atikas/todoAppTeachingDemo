using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TodoApp.API.Validators;

public class PasswordValidatorAttribute : ValidationAttribute
{
    public int MinimumLength { get; set; } = 4;
    public int MaximumLength { get; set; } = 60;
    public bool RequireUppercase { get; set; } = false;
    public bool RequireLowercase { get; set; } = false;
    public bool RequireDigit { get; set; } = false;
    public bool RequireSpecialCharacter { get; set; } = false;

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return new ValidationResult("Password is required.");
        }

        var password = value.ToString();

        if (string.IsNullOrEmpty(password))
        {             
            return new ValidationResult("Password is required.");
        }

        if (password.Length < MinimumLength)
        {
            return new ValidationResult($"Password must be at least {MinimumLength} characters long.");
        }

        if (password.Length > MaximumLength)
        {
            return new ValidationResult($"Password must be at most {MaximumLength} characters long.");
        }

        if (RequireUppercase && !password.Any(char.IsUpper))
        {
            return new ValidationResult("Password must contain at least one uppercase letter.");
        }

        if (RequireLowercase && !password.Any(char.IsLower))
        {
            return new ValidationResult("Password must contain at least one lowercase letter.");
        }

        if (RequireDigit && !password.Any(char.IsDigit))
        {
            return new ValidationResult("Password must contain at least one digit.");
        }

        if (RequireSpecialCharacter && password.All(char.IsLetterOrDigit))
        {
            return new ValidationResult("Password must contain at least one special character.");
        }

        return ValidationResult.Success;
    }
}


