using TodoApp.API.Dtos.Requests;
using TodoApp.API.Mappers.Interfaces;
using TodoApp.DAL.Entities;

namespace TodoApp.API.Mappers
{
    public class ImageMapper: IImageMapper
    {
        public Image Map(ImageUploadRequestDto dto, long todoItemId)
        {
            using var stream = new MemoryStream();
            dto.Image.CopyTo(stream);
            var imageBytes = stream.ToArray();
            return new Image
            {
                Name = dto.Name!,
                Description = dto.Description!,
                TodoItemId = todoItemId,
                ImageBytes = imageBytes,
            };
        }
    }
}