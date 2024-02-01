using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using TodoApp.API.Dtos;
using TodoApp.API.Dtos.Results;
using TodoApp.API.Mappers.Interfaces;
using TodoApp.BLL.Services.Interfaces;
using TodoApp.DAL.Repositories.Interfaces;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly ITodoRepository _repository;
        private readonly ITodoItemMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly Guid _userId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TodoController(ILogger<TodoController> logger,
            ITodoRepository repository,
            IHttpContextAccessor httpContextAccessor,
            ITodoItemMapper mapper,
            IEmailService emailService)
        {
            _logger = logger;
            _repository = repository;
            _userId = new Guid(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _mapper = mapper;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// gets all todos for a user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TodoItemResultDto>), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Get()
        {
            _logger.LogInformation($"Getting all todos for user {_userId}");
            var entities = _repository.GetAll().Where(e => e.AccountId == _userId);
            var dtos = _mapper.Map(entities);
            return Ok(dtos);
        }

        /// <summary>
        /// gets a todoitem for a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoItemResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Get(long id)
        {
            _logger.LogInformation($"Getting todo with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Todo with id {id} for user {_userId} not found");
                return NotFound();
            }
            var dto = _mapper.Map(entity);
            return Ok(dto);
        }

        /// <summary>
        /// creates a todoitem for a user
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Post(TodoItemRequestDto req)
        {
            _logger.LogInformation($"Creating todo for user {_userId} with Title {req.Title}");
            var entity = _mapper.Map(req);
            _repository.Add(entity);

            var email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            var isSent = _emailService.SendEmail(email, $"Created new TODO item: {entity.Title}");
            if (!isSent)
            {
                _logger.LogError($"Failed to send email to {email}");
            }

            return Created(nameof(Get), new { id = entity.Id });
        }

        /// <summary>
        /// updates a todoitem for a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(long id, TodoItemRequestDto req)
        {
            _logger.LogInformation($"Updating todo with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Todo with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"Todo with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// deletes a todoitem for a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation($"Deleting todo with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Todo with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"Todo with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _repository.Delete(entity);
            return NoContent();
        }
    }
}
