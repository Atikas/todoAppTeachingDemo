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
        /// username input validation RBVT
        /// </summary>
        public static IEnumerable<object[]> UserNameValidationData =>
           new List<object[]>
           {
                new object[] { null, false }, // required validation for the password input testing
                new object[] { "ab", false }, // value just below minimum boundry
                new object[] { "abc", true }, // value on minimum boundry
                new object[] { "abcd", true }, // value just above minimum boundry
                new object[] { new string('a', 49), true }, // value just below maximum boundry
                new object[] { new string('a', 50), true }, // value on maximum boundry
                new object[] { new string('a', 51), false }, // value just above maximum 
                new object[] { new string('a', 25), true }, // value inside middle of boundry
           };

       
        [Theory]
        [MemberData(nameof(UserNameValidationData))]
        public void UserName_ValidationTests(string userName, bool expectedIsValid)
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = userName,
                Password = "P@$$w0rd",
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }


        //---------------------------------------------------------------------------


        /// <summary>
        /// password input validation RBVT
        /// </summary>
        public static IEnumerable<object[]> PasswordValidationData =>
            new List<object[]>
            {
                new object[] { null, false }, // required validation for the password input testing
                new object[] { "A1!", false }, // value just below minimum boundry
                new object[] { "A1!d", true }, // value on minimum boundry
                new object[] { "A1!de", true }, // value just above minimum boundry
                new object[] { "A1!" + new string('a', 56), true }, // value just below maximum boundry
                new object[] { "A1!" + new string('a', 57), true }, // value on maximum boundry
                new object[] { "A1!" + new string('a', 58), false }, // value just above maximum 
                new object[] { "A1!" + new string('a', 27), true }, // value inside middle of boundry
                new object[] { "password", true }, // upper case requirement for the password input testing
                new object[] { "PASSWORD", true }, // lower case requirement for the password input testing
                new object[] { "Password", true }, // digit requirement for the password input testing
                new object[] { "Password1", true }, // special character requirement for the password input testing
            };

        [Theory]
        [MemberData(nameof(PasswordValidationData))]
        public void Password_ValidationTests(string password, bool expectedIsValid)
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = password,
                Role = "User"
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }

        //---------------------------------------------------------------------------


        /// <summary>
        /// email input validation RBVT
        /// </summary>
        public static IEnumerable<object[]> EmailValidationData =>
            new List<object[]>
            {
                //Invalid Email Addresses (Outside Boundaries):
                new object[] { "", false }, // (empty string): Tests absence of input.
                new object[] { "test", false }, // Email without @ is invalid
                new object[] { "@domain.com", false }, // Missing local part.
                new object[] { "test@", false }, // Missing domain part
                new object[] { "@test", false }, // Email without user part is invalid
                new object[] { "test@domain", false }, // Missing top-level domain.
                new object[] { "test@domain.", false }, // Email with . at the end is invalid
                new object[] { "test@.com", false }, // Domain starts with a prohibited character.
                //TODO new object[] { "test@domain..com", false }, // Double dot in domain.
                new object[] { "test@domain.com.", false }, // Domain ends with a dot.
                //TODO new object[] { "test@domain.com.a", false }, //TLD is one character (may be considered valid in some contexts, but typically TLDs are at least two characters).
                //TODO new object[] { "test@-domain.com", false }, // Domain starts with a prohibited character (hyphen).
                //TODO new object[] { "test@domain.com-", false }, //Domain ends with a prohibited character (hyphen).
                //Valid Email Addresses (Within Boundaries):
                new object[] { null, true }, // optional email input testing
                new object[] { "test@domain.com", true }, // valid email input testing
                //Email Addresses Testing Boundary Conditions:
                new object[] { "a@b.com", true }, // Minimal valid email (shortest possible username and domain with a single-character each).
                new object[] { "test@" + new string('a', 255) + ".com", true }, // Maximum length of domain  part.
                new object[] { new string('a', 64) + "@domain.com", true }, // Maximum length of local part.
                new object[] { "user..name@domain.com", true }, //Double dot in the local part, which is technically valid in quoted strings
                //Special Characters Testing:
                new object[] { "user.name+tag@domain.co.uk", true }, // An email with a dot and plus sign in the local part and a two-level domain.
                new object[] { "user.name_tag@domain.co.uk", true }, //Underscore in local part.
                new object[] { "user.name-tag@domain.co.uk", true }, //Hyphen in local part.
                //TODO new object[] { "user,name@domain.co.uk", false }, //Comma is not allowed without quoting.
                //International Characters and Extended Unicode
                //TODO 用户@例子.广告
                //TODO θσερ@εχαμπλε.ψομ
                //TODO šis@lietuviškai.lt
                //Special Cases:
                new object[] { "user name@domain.com", true }, //Space in the local part, which is valid in quoted strings but often considered invalid.

            };

       
        [Theory]
        [MemberData(nameof(EmailValidationData))]
        public void Email_ValidationTests(string email, bool expectedIsValid)
        {
            // Arrange
            var accountRequestDto = new AccountRequestDto
            {
                UserName = "abc",
                Password = "P@$$w0rd",
                Role = "User",
                Email = email
            };
            var validationContext = new ValidationContext(accountRequestDto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(accountRequestDto, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
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
