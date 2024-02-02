using Microsoft.EntityFrameworkCore;
using System.Text;
using TodoApp.DAL.Entities;
using TodoApp.DAL.Repositories.Interfaces;
using Xunit;

namespace TodoApp.DAL.Repositories.Tests;

public class AccountRepositoryTests
{
    private readonly TodoAppContext context;
    private readonly IAccountRepository _accountRepository;

    public AccountRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<TodoAppContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
            .Options;
        context = new TodoAppContext(options);
        _accountRepository = new AccountRepository(context);
    }
    [Fact]
    public void Create_ValidAccount_ReturnsNonNullId()
    {
        // Arrange
        var account = new Account { UserName = "testUser", Email = "testEmail@test.com", PasswordHash = Encoding.UTF8.GetBytes("hash"), PasswordSalt = Encoding.UTF8.GetBytes("salt"), Role = "User" };

        // Act
        var result = _accountRepository.Create(account);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
    }

    [Fact]
    public void Create_NullAccount_ThrowsException()
    {

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _accountRepository.Create(null));
    }

    [Fact]
    public void Create_DuplicateUserName_ThrowsException()
    {
        // Arrange
        var account1 = new Account { UserName = "testUser", Email = "testEmail1@test.com", PasswordHash = Encoding.UTF8.GetBytes("hash"), PasswordSalt = Encoding.UTF8.GetBytes("salt"), Role = "User" };
        var account2 = new Account { UserName = "testUser", Email = "testEmail2@test.com", PasswordHash = Encoding.UTF8.GetBytes("hash"), PasswordSalt = Encoding.UTF8.GetBytes("salt"), Role = "User" };

        // Act
        _accountRepository.Create(account1);

        // Assert
        Assert.Throws<ArgumentException>(() => _accountRepository.Create(account2));
    }

    [Fact]
    public void Get_ValidUserName_ReturnsCorrectAccount()
    {
        // Arrange
        var account = new Account
        {
            UserName = "testUser",
            Email = "testEmail@test.com",
            PasswordHash = Encoding.UTF8.GetBytes("fakePasswordHash"),
            PasswordSalt = Encoding.UTF8.GetBytes("fakePasswordSalt"),
            Role = "user"
        };
        _accountRepository.Create(account);

        // Act
        var result = _accountRepository.Get(account.UserName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(account.UserName, result.UserName);
    }

    [Fact]
    public void Get_InvalidUserName_ReturnsNull()
    {
        // Act
        var result = _accountRepository.Get("invalidUserName");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Get_NullUserName_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _accountRepository.Get(null));
    }

    [Fact]
    public void Delete_ValidId_AccountDoesNotExist()
    {
        // Arrange
        var account = new Account
        {
            UserName = "testUser",
            Email = "testEmail@test.com",
            PasswordHash = Encoding.UTF8.GetBytes("fakePasswordHash"),
            PasswordSalt = Encoding.UTF8.GetBytes("fakePasswordSalt"),
            Role = "user"
        };
        var id = _accountRepository.Create(account);

        // Act
        _accountRepository.Delete(id);

        // Assert
        Assert.False(_accountRepository.Exists(id));
    }

    [Fact]
    public void Delete_InvalidId_NoExceptionThrown()
    {
        // Arrange
        var invalidId = Guid.NewGuid();

        // Act & Assert
        var exception = Record.Exception(() => _accountRepository.Delete(invalidId));
        Assert.Null(exception);
        Assert.False(_accountRepository.Exists(invalidId));
    }




}