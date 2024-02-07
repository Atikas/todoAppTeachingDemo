using System.ComponentModel.DataAnnotations;
using TodoApp.API.Dtos;
using Xunit;

namespace TodoApp.APITests.Dtos.Requests
{
    public class TodoItemRequestDtoTests
    {
        [Fact]
        public void Type_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = null, //<-- this is testing value
                Title = "Title",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Type_WhenEmpty_ShouldFailValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "", //<-- this is testing value
                Title = "Title",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Type_WhenLength101_ShouldFailValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = new string('a', 101), //<-- this is testing value
                Title = "Title",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Type_WhenHoliday_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday", //<-- this is testing value
                Title = "Title",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Type_WhenWork_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Work", //<-- this is testing value
                Title = "Title",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Type_WhenShopping_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Shopping", //<-- this is testing value
                Title = "Title",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Type_WhenOther_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Other", //<-- this is testing value
                Title = "Title",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Type_WhenUnkownType_ShouldFailValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "UnkownType", //<-- this is testing value
                Title = "Title",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        //------------------- Title -------------------

        [Fact]
        public void Title_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = null, //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Title_WhenEmpty_ShouldFailValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "", //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Title_WhenLength101_ShouldFailValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = new string('a', 101), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Title_WhenLength100_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = new string('a', 100), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        //------------------- Description -------------------

        [Fact]
        public void Description_WhenNull_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                Description = null, //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Description_WhenEmpty_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                Description = "", //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Description_WhenLength1001_ShouldFailValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                Description = new string('a', 1001), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Description_WhenLength1000_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                Description = new string('a', 1000), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        //------------------- Place -------------------

        [Fact]
        public void Place_WhenNull_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                City = null, //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Place_WhenEmpty_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                City = "", //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Place_WhenLength101_ShouldFailValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                City = new string('a', 101), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Place_WhenLength100_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                City = new string('a', 100), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        //------------------- Due -------------------

        [Fact]
        public void Due_WhenNull_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                Due = null, //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Due_WhenEqualToToday_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                Due = DateTime.Today, //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Due_WhenGreaterThanToday_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                Due = DateTime.Today.AddDays(1), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Due_WhenLessThanToday_ShouldFailValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                Due = DateTime.Today.AddDays(-1), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        //------------------- CompletedAt -------------------

        [Fact]
        public void CompletedAt_WhenNull_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                CompletedAt = null, //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CompletedAt_WhenEqualToToday_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                CompletedAt = DateTime.Today, //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CompletedAt_WhenGreaterThanToday_ShouldFailValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                CompletedAt = DateTime.Today.AddDays(1), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CompletedAt_WhenLessThanToday_ShouldPassValidation()
        {
            // Arrange
            var dto = new TodoItemRequestDto
            {
                Type = "Holiday",
                Title = "Title",
                CompletedAt = DateTime.Today.AddDays(-1), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

    }
}
