using Xunit;
using TodoApp.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TodoApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using TodoApp.API.Mappers.Interfaces;
using Moq;
using TodoApp.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApp.DAL.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using TodoApp.API.Dtos;
using TodoApp.API.Mappers;
using Microsoft.Extensions.Configuration.UserSecrets;
using TodoApp.API.Dtos.Results;
using System.Linq.Expressions;

namespace TodoApp.API.Controllers.Tests
{
    public class TodoControllerTests
    {
        private readonly Guid userId;
        private readonly TodoController controller;
        private readonly Mock<ITodoRepository> repository;
        private readonly TodoItemMapper mapper;
        public TodoControllerTests()
        {
            var logger = new Mock<ILogger<TodoController>>();
            repository = new Mock<ITodoRepository>();
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var emailService = new Mock<IEmailService>();
            userId = Guid.NewGuid();

            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.NameIdentifier, userId.ToString() ) ,
                new Claim(ClaimTypes.Name, "Test"),
                new Claim(ClaimTypes.Email, "test@example.com")

            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            httpContextAccessor.Setup(x => x.HttpContext).Returns(new Mock<HttpContext>().Object);
            httpContextAccessor.Setup(x => x.HttpContext.User).Returns(principal);
            
            mapper = new TodoItemMapper(httpContextAccessor.Object);
            controller = new TodoController(logger.Object, repository.Object, httpContextAccessor.Object, mapper, emailService.Object);

        }

        // ------------ GetAll Tests ------------

        //GetAll method when repository returns empty array should return type OkObjectResult with empty array 
        [Fact]
        public void GetAll_WhenEmptyArray_ReturnsOk()
        {
            // Arrange
            repository.Setup(x => x.GetAll(It.IsAny<Expression<Func<TodoItem, object>>>())).Returns(Enumerable.Empty<TodoItem>().AsQueryable());

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Empty((result as OkObjectResult).Value as List<TodoItemResultDto>);
        }

        //GetAll method when repository returns empty array should return type OkObjectResult with not empty array 
        [Fact]
        public void GetAll_WhenNotEmptyArray_ReturnsOk()
        {
            // Arrange
            repository.Setup(x => x.GetAll(It.IsAny<Expression<Func<TodoItem, object>>>())).Returns(new List<TodoItem> { new TodoItem { AccountId = userId } }.AsQueryable());

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotEmpty((result as OkObjectResult).Value as List<TodoItemResultDto>);
        }

        // ------------ Get Tests ------------

        //Get method when repository returns entity should return type OkObjectResult with dto instance
        [Fact]
        public void Get_WhenIdIsValid_ReturnsOk()
        {
            // Arrange
            var id = 1;
            repository.Setup(x => x.Get(id)).Returns(new TodoItem { Id = 1, AccountId = userId });

            // Act
            var result = controller.Get(id).Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<TodoItemResultDto>((result as OkObjectResult).Value);
        }

        //Get method when repository returns null should return type NotFoundResult
        [Fact]
        public void Get_WhenIdIsInvalid_ReturnsNotFound()
        {
            // Arrange
            var id = 1;

            // Act
            var result = controller.Get(id).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // ------------ Post Tests ------------

        //Post method when valid should return type CreatedResult
        [Fact]
        public void Post_WhenValid_ReturnsCreated()
        {
            // Arrange
            var dto = new TodoItemRequestDto();

            // Act
            var result = controller.Post(dto);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        // ------------ Put Tests ------------

        //Put method when valid should return type NoContentResult
        [Fact]
        public void Put_WhenValid_ReturnsNoContent()
        {
            // Arrange
            var id = 1;
            var dto = new TodoItemRequestDto();
            var entity = new TodoItem { AccountId = userId };
            repository.Setup(x => x.Get(id)).Returns(entity);

            // Act
            var result = controller.Put(id, dto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        //Put method when invalid id should return type NotFoundResult
        [Fact]
        public void Put_WhenInvalidId_ReturnsNotFound()
        {
            // Arrange
            var id = 1;
            var dto = new TodoItemRequestDto();
            repository.Setup(x => x.Get(id)).Returns((TodoItem)null);

            // Act
            var result = controller.Put(id, dto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //Put method when invalid account id should return type ForbiddenResult
        [Fact]
        public void Put_WhenInvalidAccountId_ReturnsForbidden()
        {
            // Arrange
            var id = 1;
            var dto = new TodoItemRequestDto();
            var entity = new TodoItem { AccountId = Guid.NewGuid() };
            repository.Setup(x => x.Get(id)).Returns(entity);

            // Act
            var result = controller.Put(id, dto);

            // Assert
            Assert.IsType<ForbidResult>(result);
        }

    }
}