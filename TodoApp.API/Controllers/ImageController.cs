using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using TodoApp.API.Dtos.Requests;
using TodoApp.API.Dtos.Results;
using TodoApp.API.Mappers.Interfaces;
using TodoApp.DAL.Repositories.Interfaces;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IImageRepository _imageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageMapper _mapper;
        private readonly ITodoRepository _todoRepository;

        private readonly Guid _userId;
        public ImageController(ILogger<ImageController> logger,
            IImageRepository imageRepository,
            IHttpContextAccessor httpContextAccessor,
            IImageMapper mapper,
            ITodoRepository todoRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userId = new Guid(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _todoRepository = todoRepository;
        }

        /// <summary>
        /// gets all images for a todoitem for a user
        /// </summary>
        /// <param name="todoItemId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/Todo/{todoItemId}/[controller]")]
        [ProducesResponseType(typeof(List<ImageResultDto>), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetImagesForTodo([FromRoute]long todoItemId)
        {
            _logger.LogInformation($"Getting all images for user {_userId}");
            var todoEntity = _todoRepository.Get(todoItemId);
            if (todoEntity == null)
            {
                _logger.LogInformation($"Todo with id {todoItemId} for user {_userId} not found");
                return NotFound("Todo item not found");
            }
            if (todoEntity.AccountId != _userId)
            {
                _logger.LogInformation($"Todo with id {todoItemId} for user {_userId} is forbidden");
                return Forbid();
            }
            var entities = _imageRepository.GetAll().Where(e => e.TodoItemId == todoItemId);
            var dtos = _mapper.Map(entities);
            return Ok(dtos);
        }

        /// <summary>
        /// get an image for a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Produces(MediaTypeNames.Image.Png)]
        public IActionResult Get(long id)
        {
            _logger.LogInformation($"Getting image {id} for user {_userId}");
            var entity = _imageRepository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Image {id} not found for user {_userId}");
                return NotFound();
            }
            if (entity.TodoItem.AccountId != _userId)
            {
                _logger.LogInformation($"Image {id} is forbidden for user {_userId}");
                return Forbid();
            }
            return File(entity.ImageBytes, $"image/png");
        }

        /// <summary>
        /// creates an image for a user
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/Todo/{todoItemId}/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Post([FromRoute]long todoItemId, [FromForm] ImageUploadRequestDto req)
        {
            _logger.LogInformation($"Creating image for user {_userId}");
            var todoEntity = _todoRepository.Get(todoItemId);
            if (todoEntity == null)
            {
                _logger.LogInformation($"Todo with id {todoItemId} for user {_userId} not found");
                return NotFound("Todo item not found");
            }
            if (todoEntity.AccountId != _userId)
            {
                _logger.LogInformation($"Todo with id {todoItemId} for user {_userId} is forbidden");
                return Forbid();
            }

            var image = _mapper.Map(req, todoItemId);
           _imageRepository.Add(image);

            return Created(nameof(Get), new { id = image.Id});
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation($"Deleting image {id} for user {_userId}");
            var entity = _imageRepository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Image {id} not found for user {_userId}");
                return NotFound();
            }
            if (entity.TodoItem.AccountId != _userId)
            {
                _logger.LogInformation($"Image {id} is forbidden for user {_userId}");
                return Forbid();
            }
            _imageRepository.Delete(entity);
            return NoContent();
        }
    }
}
