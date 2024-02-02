using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApp.API.Services.Interfaces;
using TodoApp.DAL.Entities;

namespace TodoApp.API.Services;

public class JwtService : IJwtService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    public JwtService(IConfiguration conf)
    {
        _secretKey = conf.GetValue<string>("Jwt:Key") ?? "";
        _issuer = conf.GetSection("Jwt:Issuer").Value ?? "";
        _audience = conf.GetSection("Jwt:Audience").Value ?? "";
    }
    public string GetJwtToken(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.NameIdentifier, account.Id.ToString()),
                new (ClaimTypes.Name, account.UserName.ToString()),
                new (ClaimTypes.Role, account.Role),
                new (ClaimTypes.Email, account.Email)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _issuer,
            Audience = _audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
