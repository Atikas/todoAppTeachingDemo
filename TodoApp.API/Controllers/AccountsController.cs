using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TodoApp.API.Dtos.Requests;
using TodoApp.API.Mappers.Interfaces;
using TodoApp.API.Services.Interfaces;
using TodoApp.BLL.Services.Interfaces;
using TodoApp.DAL.Repositories.Interfaces;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountRepository _repository;
        private readonly IJwtService _jwtService;
        private readonly IAccountMapper _mapper;
        private readonly IAccountService _service;

        public AccountsController(ILogger<AccountsController> logger,
            IAccountRepository repository,
            IJwtService jwtService,
            IAccountMapper mapper,
            IAccountService service)
        {
            _logger = logger;
            _repository = repository;
            _jwtService = jwtService;
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        ///  create a user account
        /// </summary>
        /// <param name="req"></param>
        /// <response code="500">System error</response>
        [HttpPost("SignUp")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult SignUp(AccountRequestDto req)
        {
            _logger.LogInformation($"Creating account for {req.UserName}");
            var account = _mapper.Map(req);
            var userId = _repository.Create(account);
            _logger.LogInformation($"Account for {req.UserName} created with id {userId}");
            return Created("", new {id = userId });
        }

        /// <summary>
        ///  user login
        /// </summary>
        /// <response code="400">Model validation error</response>
        /// <response code="500">System error</response>
        [HttpPost("Login")]
        [Produces(MediaTypeNames.Text.Plain)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Login(LoginRequestDto req)
        {
            _logger.LogInformation($"Login attempt for {req.UserName}");
            var account = _repository.Get(req.UserName!);
            if (account == null)
            {
                _logger.LogWarning($"User {req.UserName} not found");
                return BadRequest("User nor found");
            }

            var isPasswordValid = _service.VerifyPasswordHash(req.Password, account.PasswordHash, account.PasswordSalt);

            if (!isPasswordValid)
            {
                _logger.LogWarning($"Invalid password for {req.UserName}");
                return BadRequest("Invalid username or password");
            }
            _logger.LogInformation($"User {req.UserName} successfully logged in");
            var jwt = _jwtService.GetJwtToken(account);
            return Ok(jwt);
            
        }
    }
}
