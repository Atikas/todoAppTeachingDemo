using System.Security.Claims;

namespace TodoApp.API.Services
{
    public class AuthorizedUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string? GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
        }
    }
}
