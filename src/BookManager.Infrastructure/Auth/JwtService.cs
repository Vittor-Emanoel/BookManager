using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Book_manager.src.BookManager.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtService : IJwtService
{
  public readonly IConfiguration _config;

  public JwtService(IConfiguration config)
  {
    _config = config;
  }


  public string GenerateToken(Guid Id, string name, string email)
  {
    var key = _config["Jwt:key"]!;
    var issuer = _config["Jwt:issuer"]!;
    var audience = _config["Jwt:audience"]!;

    /// Payload do token
    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, Id.ToString()),
      new Claim(JwtRegisteredClaimNames.Email, email),
      new Claim("name", name),
    };

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


    var expires = DateTime.UtcNow.AddHours(5);

    var token = new JwtSecurityToken(
      issuer,
      audience,
      claims,
      expires,
      signingCredentials: creds
    );


    return new JwtSecurityTokenHandler().WriteToken(token);

  }
}