using System.ComponentModel.DataAnnotations;
using TodoApp.API.Validators;

namespace TodoApp.API.Dtos.Requests
{
    public class ImageUploadRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions([".png"])]
        public IFormFile Image { get; set; } = null!;
    }
}
