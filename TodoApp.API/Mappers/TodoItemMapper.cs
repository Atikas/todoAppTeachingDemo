using TodoApp.API.Dtos;
using TodoApp.API.Mappers.Interfaces;
using TodoApp.DAL.Entities;

namespace TodoApp.API.Mappers
{
    public class TodoItemMapper: ITodoItemMapper
    {
        public TodoItemResultDto Map(TodoItem entity)
        {
            return new TodoItemResultDto
            {
                Id = entity.Id,
                Type = entity.Type,
                Title = entity.Title,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                Due = entity.Due,
                CompletedAt = entity.CompletedAt
            };
        }
        public List<TodoItemResultDto> Map(IEnumerable<TodoItem> entities)
        {
            return entities.Select(Map).ToList();
        }
    }
}
