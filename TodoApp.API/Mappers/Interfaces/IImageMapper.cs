using TodoApp.API.Dtos.Requests;
using TodoApp.DAL.Entities;

namespace TodoApp.API.Mappers.Interfaces
{
    public interface IImageMapper
    {
        Image Map(ImageUploadRequestDto dto, long todoItemId);
    }
}