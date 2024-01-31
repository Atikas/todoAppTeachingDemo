using TodoApp.API.Dtos;
using TodoApp.DAL.Entities;

namespace TodoApp.API.Mappers.Interfaces
{
    public interface ITodoItemMapper
    {
        TodoItemResultDto Map(TodoItem entity);
        List<TodoItemResultDto> Map(IEnumerable<TodoItem> entities);
    }
}
