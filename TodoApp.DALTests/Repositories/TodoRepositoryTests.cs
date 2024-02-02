using Microsoft.EntityFrameworkCore;
using System.Text;
using TodoApp.DAL.Entities;
using Xunit;

namespace TodoApp.DAL.Repositories.Tests
{
    public class TodoRepositoryTests
    {
        private readonly TodoAppContext _context;
        private readonly TodoRepository _todoRepository;

        public TodoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TodoAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
                .Options;
            //Ensure that the data seeding is skipped when the context is created
            _context = new TodoAppContext(options) { SkipSeeding = true };
            _todoRepository = new TodoRepository(_context);
        }

        [Fact]
        public void GetAll_NoTodoItems_ReturnsEmpty()
        {
            // Act
            var result = _todoRepository.GetAll();

            // Assert
            Assert.Empty(result);
        }
        [Fact]
        public void GetAll_SomeTodoItems_ReturnsAllTodoItems()
        {
            // Arrange
            var todoItem1 = new TodoItem
            {
                Id = 1,
                Type = "Other",
                Title = "TodoItem1",
                Description = "Description1",
                CreatedAt = DateTime.Now,

            };
            var todoItem2 = new TodoItem
            {
                Id = 2,
                Type = "Other",
                Title = "TodoItem2",
                Description = "Description2",
                CreatedAt = DateTime.Now,

            };
            _context.TodoItems.Add(todoItem1);
            _context.TodoItems.Add(todoItem2);
            _context.SaveChanges();

            // Act
            var result = _todoRepository.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void Get_ValidId_ReturnsCorrectTodoItem()
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = 1,
                Type = "Other",
                Title = "TodoItem1",
                Description = "Description1",
                CreatedAt = DateTime.Now,

            };
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();

            // Act
            var result = _todoRepository.Get(1);

            // Assert
            Assert.Equal(todoItem, result);
        }
        [Fact]
        public void Get_InvalidId_ReturnsNull()
        {
            // Act
            var result = _todoRepository.Get(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_ValidTodoItem_TodoItemIsAdded()
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = 1,
                Type = "Other",
                Title = "TodoItem1",
                Description = "Description1",
                CreatedAt = DateTime.Now,
            };

            // Act
            _todoRepository.Add(todoItem);

            // Assert
            Assert.Equal(todoItem, _context.TodoItems.Find(todoItem.Id));
        }

        [Fact]
        public void Update_ValidTodoItem_TodoItemIsUpdated()
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = 1,
                Type = "Other",
                Title = "TodoItem1",
                Description = "Description1",
                CreatedAt = DateTime.Now,
            };
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();

            todoItem.Title = "NewTitle";

            // Act
            _todoRepository.Update(todoItem);

            // Assert
            Assert.Equal("NewTitle", _context.TodoItems.Find(todoItem.Id)?.Title);
        }

        [Fact]
        public void Update_TodoitemNotInDatabase_ThrowsException()
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = 1,
                Type = "Other",
                Title = "TodoItem1",
                Description = "Description1",
                CreatedAt = DateTime.Now,
            };

            // Act & Assert
            Assert.Throws<DbUpdateConcurrencyException>(() => _todoRepository.Update(todoItem));

        }

        [Fact]
        public void Delete_ValidId_TodoItemDoesNotExist()
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = 1,
                Type = "Other",
                Title = "TodoItem1",
                Description = "Description1",
                CreatedAt = DateTime.Now,

            };
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();

            // Act
            _todoRepository.Delete(todoItem);

            // Assert
            Assert.Null(_context.TodoItems.Find(todoItem.Id));
        }

        [Fact]
        public void Delete_ValidId_TodoItemAndImageDoesNotExist()
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = 1,
                Type = "Other",
                Title = "TodoItem1",
                Description = "Description1",
                CreatedAt = DateTime.Now,

            };
            var image = new Image
            {
                Id = 1,
                Name = "Image1",
                Description = "Description1",
                ImageBytes = Encoding.UTF8.GetBytes("ImageBytes1"),
                TodoItemId = 1
            };
            _context.TodoItems.Add(todoItem);
            _context.Images.Add(image);
            _context.SaveChanges();

            // Act
            _todoRepository.Delete(todoItem);

            // Assert
            Assert.Null(_context.TodoItems.Find(todoItem.Id));
            Assert.Null(_context.Images.Find(image.Id));

        }


    }
}