using System.ComponentModel.DataAnnotations;
using TodoApp.API.Dtos.Requests;
using Xunit;

namespace TodoApp.APITests.Dtos.Requests
{
    /// <summary>
    /// Robust Boundary Value Testing (RBVT) implementation for AccountRequestDto
    /// </summary>
    public class AccountRequestDtoTests
    {

        /// <summary>
        /// required validation for the username input testing
        /// </summary>
        [Fact] 
        public void Username_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = null,
                Password = "P@$$w0rd",
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// minimum value that the username input can legally take
        /// </summary>
        [Fact] 
        public void UserName_WhenLength3_ShouldPassValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto 
            { 
                UserName = "abc", 
                Password = "P@$$w0rd", 
                Role = "User" 
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// value just outside minimum the username legal boundaries of the input domains
        /// </summary>
        [Fact] 
        public void UserName_WhenLength2_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "ab",
                Password = "P@$$w0rd",
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// maximum value that the username input can legally take
        /// </summary>
        [Fact] 
        public void UserName_WhenLength50_ShouldPassValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = new string('a', 50), 
                Password = "P@$$w0rd", 
                Role = "User" 
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);

        }

        /// <summary>
        /// value just outside maximum the legal boundaries of the username input domains
        /// </summary>
        [Fact] 
        public void UserName_WhenLength51_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = new string('a', 51),
                Password = "P@$$w0rd",
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);

        }

        /// <summary>
        ///  value inside middle of the legal boundaries of the username input domains
        /// </summary>
        [Fact] 
        public void UserName_WhenLength25_ShouldPassValidationd()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = new string('a', 25),
                Password = "P@$$w0rd",
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        //------------ Password Validation Tests ------------
        
        /// <summary>
        /// required validation for the password input testing
        /// </summary>
        [Fact] 
        public void Password_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = null,
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// minimum value that the password input can legally take
        /// </summary>
        [Fact] 
        public void Password_WhenLength4_ShouldPassValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "abcd",
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// value just outside minimum the password legal boundaries of the input domains
        /// </summary>
        [Fact]
        public void Password_WhenLength3_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "abc",
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// value inside of the legal boundaries of the password input domains
        /// </summary>
        [Fact]
        public void Password_WhenLength50_ShouldPassValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = new string('a', 50),
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// upper case requirmenet for the password input testing
        /// </summary>
        [Fact]
        public void Password_WhenNoUppercase_ShouldPassValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "password",
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// lower case requirmenet for the password input testing
        /// </summary>
        [Fact]
        public void Password_WhenNoLowercase_ShouldPassValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "PASSWORD",
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// digit requirmenet for the password input testing
        /// </summary>
        [Fact]
        public void Password_WhenNoDigit_ShouldPassValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "Password",
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }


        /// <summary>
        /// special character requirmenet for the password input testing
        /// </summary>
        [Fact]
        public void Password_WhenNoSpecialCharacter_ShouldPassValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "Password1",
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }


        //------------ Email Validation Tests ------------

        /// <summary>
        /// optional email input testing
        /// </summary>
        [Fact]
        public void Email_WhenNull_ShouldPassValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = "User",
                Email = null
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }


        [Fact]
        public void Email_WhenValid_ShouldPassValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = "User",
                Email = "user@example.com",
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Email_WhenTopLevelDomainMissing_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = "User",
                Email = "user@example",
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Email_WhenDomainMissing_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = "User",
                Email = "user@.com",
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Email_WhenAtSymbolMissing_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = "User",
                Email = "userexample.com",
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Email_WhenLocalPartMissing_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = "User",
                Email = "@example.com",
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Email_WhenEmpty_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = "User",
                Email = "",
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        //------------ Role Validation Tests ------------

        [Fact]
        public void Role_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = null,
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Role_WhenAdmin_ShouldPassValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = "Admin",
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Role_WhenUser_ShouldPassValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = "User",
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Role_WhenOther_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = "Other",
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Role_WhenEmpty_ShouldFailValidation()
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = "",
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }
    }
}
