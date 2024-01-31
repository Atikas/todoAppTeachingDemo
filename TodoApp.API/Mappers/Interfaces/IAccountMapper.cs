using TodoApp.API.Dtos;
using TodoApp.DAL.Entities;

namespace TodoApp.API.Mappers.Interfaces
{
    public interface IAccountMapper
    {
        Account Map(AccountRequestDto dto);
    }
}
