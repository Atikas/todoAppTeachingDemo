using TodoApp.API.Dtos;
using TodoApp.API.Dtos.Results;
using TodoApp.DAL.Entities;
using TodoApp.DAL.Models;

namespace TodoApp.API.Mappers.Interfaces
{
    public interface ITodoItemMapper
    {
        TodoItemResultDto Map(TodoItem entity);
        List<TodoItemResultDto> Map(IEnumerable<TodoItem> entities);
        TodoItem Map(TodoItemRequestDto dto);
        void ProjectTo(TodoItemRequestDto from, TodoItem to);
    }
}
