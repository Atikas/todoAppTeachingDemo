using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApp.API.Mappers.Interfaces;
using TodoApp.DAL.Repositories.Interfaces;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IImageRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly Guid _userId;
        public ImageController(ILogger<ImageController> logger,
            IImageRepository repository,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _userId = new Guid(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }


        [HttpGet("{id}")]
        public IActionResult GetImage(long id)
        {
            _logger.LogInformation($"Getting image {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                return NotFound();
            }
            if (entity.TodoItem.AccountId != _userId)
            {
                return Forbid();
            }
            return File(entity.ImageBytes, $"image/png");
        }
    }
}
