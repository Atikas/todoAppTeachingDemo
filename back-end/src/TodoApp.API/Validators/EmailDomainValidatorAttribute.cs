﻿using System.ComponentModel.DataAnnotations;

namespace TodoApp.API.Validators;

public class EmailDomainValidatorAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var email = value as string;
        if (!string.IsNullOrEmpty(email))
        {
            //count letters between @ and .
            var atIndex = email.IndexOf('@');
            if (atIndex == -1)
                return new ValidationResult("Invalid email address.");

            var tldIndex = email.LastIndexOf('.');
            if (tldIndex == -1)
                return new ValidationResult("Invalid top-level domain.");

            if (tldIndex <= atIndex+1)
                return new ValidationResult("Invalid top-level domain.");
        }

        return ValidationResult.Success!;
    }
}

