using Microsoft.AspNetCore.Mvc;
using TodoApp.API.Dtos;
using TodoApp.API.Services.Interfaces;
using TodoApp.BLL.Services.Interfaces;
using TodoApp.DAL.Repositories.Interfaces;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IAccountService _service;
        private readonly IJwtService _jwtService;

        public AccountsController(IAccountRepository repository, IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }


        [HttpPost("SignUp")]
        public ActionResult SignUp(AccountRequestDto req)
        {
            _repository.Create();
            return Ok();
        }


        [HttpPost("Login")]
        public ActionResult Login(LoginRequestDto req)
        {
            var loginSuccess = _repository.TryLogin(req.UserName, req.Password, out string? role);

            if (!loginSuccess)
            {
                return BadRequest("Invalid username or password");
            }
            else
            {
                var jwt = _jwtService.GetJwtToken(req.UserName, role!);
                return Ok(jwt);
            }
        }
    }
}
