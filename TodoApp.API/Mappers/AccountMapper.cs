using TodoApp.API.Dtos.Requests;
using TodoApp.API.Mappers.Interfaces;
using TodoApp.BLL.Services.Interfaces;
using TodoApp.DAL.Entities;

namespace TodoApp.API.Mappers
{
    public class AccountMapper: IAccountMapper
    {
        private readonly IAccountService _service;

        public AccountMapper(IAccountService service)
        {
            _service = service;
        }

        public Account Map(AccountRequestDto dto)
        {
            _service.CreatePasswordHash(dto.Password!, out var passwordHash, out var passwordSalt);
            return new Account
            {
                UserName = dto.UserName!,
                Email = dto.Email!,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = dto.Role!
            };
        }

    }
}
