using Microsoft.EntityFrameworkCore;
using System.Text;
using TodoApp.DAL.Entities;
using Xunit;

namespace TodoApp.DAL.Repositories.Tests;

public class ImageRepositoryTests
{
    private readonly TodoAppContext _context;
    private readonly ImageRepository _imageRepository;

    public ImageRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<TodoAppContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
            .Options;
        //Ensure that the data seeding is skipped when the context is created
        _context = new TodoAppContext(options) { SkipSeeding = true };
        _imageRepository = new ImageRepository(_context);
    }

    [Fact]
    public void GetAll_NoImages_ReturnsEmpty()
    {
        // Act
        var result = _imageRepository.GetAll();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetAll_SomeImages_ReturnsAllImages()
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
        var image1 = new Image
        {
            Id = 1,
            Name = "Image1",
            Description = "Description1",
            ImageBytes = Encoding.UTF8.GetBytes("ImageBytes1"),
            TodoItemId = 1
        };
        var image2 = new Image
        {
            Id = 2,
            Name = "Image2",
            Description = "Description2",
            ImageBytes = Encoding.UTF8.GetBytes("ImageBytes2"),
            TodoItemId = 1
        };
        _context.TodoItems.Add(todoItem);
        _context.Images.AddRange(image1, image2);
        _context.SaveChanges();

        // Act
        var result = _imageRepository.GetAll(i => i.TodoItem).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, i => i.Name == "Image1");
        Assert.Contains(result, i => i.Name == "Image2");
    }

    [Fact] public void GetAll_IncludeTodoItem_ReturnsImageWithTodoItem()
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
        var image1 = new Image
        {
            Id = 1,
            Name = "Image1",
            Description = "Description1",
            ImageBytes = Encoding.UTF8.GetBytes("ImageBytes1"),
            TodoItemId = 1
        };
        _context.TodoItems.Add(todoItem);
        _context.Images.Add(image1);
        _context.SaveChanges();

        // Act
        var result = _imageRepository.GetAll(i => i.TodoItem).First();

        // Assert
        Assert.NotNull(result.TodoItem);
        Assert.Equal("TodoItem1", result.TodoItem.Title);
    }
    [Fact] public void Get_ValidId_ReturnsCorrectImage()
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
        var result = _imageRepository.Get(image.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(image.Name, result.Name);

    }
    [Fact] public void Get_InvalidId_ReturnsNull()
    {
        // Act
        var result = _imageRepository.Get(1);

        // Assert
        Assert.Null(result);
    }
    [Fact] public void Add_ValidImage_ReturnsNonNullId()
    {
        // Arrange
        var image = new Image
        {
            Name = "Image1",
            Description = "Description1",
            ImageBytes = Encoding.UTF8.GetBytes("ImageBytes1"),
            TodoItemId = 1
        };

        // Act
        _imageRepository.Add(image);

        // Assert
        Assert.NotEqual(0, image.Id);
    }
    [Fact] public void Update_ValidImage_ChangesAreSaved()
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
        image.Name = "NewName";
        _imageRepository.Update(image);

        // Assert
        var result = _imageRepository.Get(image.Id);
        Assert.Equal("NewName", result.Name);
    }
    [Fact] public void Update_ImageNotInDatabase_ThrowsException()
    {
        // Arrange
        var image = new Image
        {
            Id = 1,
            Name = "Image1",
            Description = "Description1",
            ImageBytes = Encoding.UTF8.GetBytes("ImageBytes1"),
            TodoItemId = 1
        };

        // Act & Assert
        Assert.Throws<DbUpdateConcurrencyException>(() => _imageRepository.Update(image));
    }
    [Fact] public void Delete_ValidId_ImageDoesNotExist()
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
        _imageRepository.Delete(image);

        // Assert
        Assert.Null(_context.Images.Find(image.Id));
    }
    [Fact] public void Delete_ImageNotInDatabase_ThrowsException()
    {
        // Arrange
        var image = new Image
        {
            Id = 1,
            Name = "Image1",
            Description = "Description1",
            ImageBytes = Encoding.UTF8.GetBytes("ImageBytes1"),
            TodoItemId = 1
        };

        // Act & Assert
        Assert.Throws<DbUpdateConcurrencyException>(() => _imageRepository.Delete(image));
    }
    [Fact] public void Delete_ValidId_ImageDoesNotExistButTodoItemExist()
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
        _imageRepository.Delete(image);

        // Assert
        Assert.Null(_context.Images.Find(image.Id));
        Assert.NotNull(_context.TodoItems.Find(todoItem.Id));

    }
}