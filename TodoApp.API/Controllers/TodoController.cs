using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using TodoApp.API.Dtos;
using TodoApp.API.Mappers.Interfaces;
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
        private readonly Guid _userId;

        public TodoController(ILogger<TodoController> logger, ITodoRepository repository, IHttpContextAccessor _httpContextAccessor, ITodoItemMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _userId = new Guid(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            _mapper = mapper;
        }

        /// <summary>
        /// gets all todos for a user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TodoItemResultDto>),StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Get()
        {
            _logger.LogInformation($"Getting all todos for user {_userId}");
            var entities = _repository.GetAll().Where(e => e.AccountId == _userId);
            var dtos  = _mapper.Map(entities);
            return Ok(dtos);
        }

        /// <summary>
        /// gets a todo for a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoItemResultDto),StatusCodes.Status200OK)]
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
    }
}
