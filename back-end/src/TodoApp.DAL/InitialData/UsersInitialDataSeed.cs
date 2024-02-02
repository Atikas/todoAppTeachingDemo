using System.Security.Cryptography;
using System.Text;
using TodoApp.DAL.Entities;

namespace TodoApp.DAL.InitialData
{
    public static class UsersInitialDataSeed
    {
        public static List<Account> Accounts => new()
        {
            user1(),
            user2(),
        };

        private static void  CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        private static Account user1()
        {
            CreatePasswordHash("user1", out var passwordHash, out var passwordSalt);
            return new Account
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UserName = "user1",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
        }
        private static Account user2()
        {
            CreatePasswordHash("user2", out var passwordHash, out var passwordSalt);
            return new Account
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                UserName = "user2",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
        }

    }
}
