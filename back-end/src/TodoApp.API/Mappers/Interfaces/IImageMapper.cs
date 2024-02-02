using TodoApp.API.Dtos.Requests;
using TodoApp.API.Dtos.Results;
using TodoApp.DAL.Entities;

namespace TodoApp.API.Mappers.Interfaces
{
    public interface IImageMapper
    {
        Image Map(ImageUploadRequestDto dto, long todoItemId);
        List<ImageResultDto> Map(IEnumerable<Image> entities);
    }
}