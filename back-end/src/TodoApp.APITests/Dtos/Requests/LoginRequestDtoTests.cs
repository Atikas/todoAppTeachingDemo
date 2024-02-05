using System.ComponentModel.DataAnnotations;
using TodoApp.API.Dtos.Requests;
using Xunit;

namespace TodoApp.APITests.Dtos.Requests
{
    public class LoginRequestDtoTests
    {
        [Fact]
        public void UserName_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var dto = new LoginRequestDto
            {
                UserName = null,
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UserName_WhenLength2_ShouldFailValidation()
        {
            // Arrange
            var dto = new LoginRequestDto
            {
                UserName = "ab",
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UserName_WhenLength3_ShouldPassValidation()
        {
            // Arrange
            var dto = new LoginRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UserName_WhenLength50_ShouldPassValidation()
        {
            // Arrange
            var dto = new LoginRequestDto
            {
                UserName = new string('a', 50),
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

       [Fact]
        public void UserName_WhenLength51_ShouldFailValidation()
        {
            // Arrange
            var dto = new LoginRequestDto
            {
                UserName = new string('a', 51),
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Password_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var dto = new LoginRequestDto
            {
                UserName = "abc",
                Password = null,
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Password_WhenValid_ShouldPassValidation()
        {
            // Arrange
            var dto = new LoginRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Password_WhenInvalid_ShouldFailValidation()
        {
            // Arrange
            var dto = new LoginRequestDto
            {
                UserName = "abc",
                Password = "p",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }


    }
}
