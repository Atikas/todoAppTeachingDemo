using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TodoApp.API.Dtos.Results;
using TodoApp.API.Mappers;
using TodoApp.DAL.Repositories.Interfaces;
using TodoApp.DAL.Services.Services;

namespace TodoApp.API.Controllers
{
    [Route("api/Todo/{todoId}/Weather")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class TodoWeatherController : ControllerBase
    {
        private readonly ILogger<TodoWeatherController> _logger;
        private readonly ITodoRepository _repository;
        private readonly IOpenMeteoClient _openMeteoClient;
        private readonly ITodoWeatherMapper _mapper;

        public TodoWeatherController(ILogger<TodoWeatherController> logger,
            ITodoRepository repository,
            IOpenMeteoClient openMeteoClient,
            ITodoWeatherMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _openMeteoClient = openMeteoClient;
            _mapper = mapper;
        }


        /// <summary>
        /// returns the weather for a todoitem place
        /// </summary>
        /// <param name="todoId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TodoWeatherResultDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetWeather(long todoId)
        {
            var todo = _repository.Get(todoId);
            if (todo == null)
            {
                _logger.LogWarning("Todo not found with id: {todoId}", todoId);
                return NotFound("Todo not found");
            }

            var place = todo.Place;
            if (place == null)
            {
                return NoContent();
            }

            var geocodingResult = await _openMeteoClient.GetGeocodingAsync(place);
            if (geocodingResult == null)
            {
                _logger.LogWarning("GetGeocodingAsync failed with location: {location}", place);
                return NoContent();
            }
            var location = geocodingResult.results.FirstOrDefault();
            if (location != null)
            {
                var weather = await _openMeteoClient.GetWeatherForecastAsync(location.latitude, location.longitude);
                if (weather == null)
                {
                    _logger.LogWarning("GetWeatherForecastAsync failed with location: {location}", place);
                    return NoContent();
                }
                var result = _mapper.Map(weather);
                return Ok(result);
            }
            return NoContent();
        }
    }
}
