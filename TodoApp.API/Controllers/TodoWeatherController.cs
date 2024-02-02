using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.DAL.Repositories.Interfaces;
using TodoApp.DAL.Services;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoWeatherController : ControllerBase
    {
        private readonly ILogger<TodoWeatherController> _logger;
        private readonly ITodoRepository _repository;
        private readonly IOpenMeteoClient _openMeteoClient;

        public TodoWeatherController(ILogger<TodoWeatherController> logger,
            ITodoRepository repository,
            IOpenMeteoClient openMeteoClient)
        {
            _logger = logger;
            _repository = repository;
            _openMeteoClient = openMeteoClient;
        }
    }
}
