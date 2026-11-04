using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace Rest_Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IMemoryCache _sessions;

    public AuthController(IConfiguration config, IMemoryCache sessions)
    {
        _config = config;
        _sessions = sessions;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginRequest login)
    {
        // Demo-bruger til skoleprojektet.
        if (login.Username != "admin" || login.Password != "Test123!")
            return Unauthorized("Forkert brugernavn eller adgangskode.");

        return Ok(new { token = GenerateJwtToken(login.Username) });
    }

    private string GenerateJwtToken(string username)
    {
        var settings = _config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings["Key"]!));
        var id = Guid.NewGuid().ToString();
        var expires = DateTime.UtcNow.AddMinutes(30);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, id)
        };
        var token = new JwtSecurityToken(
            issuer: settings["Issuer"],
            audience: settings["Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        // Gem sessionen, så logout kan afslutte den før tokenet udløber.
        _sessions.Set(id, true, new DateTimeOffset(expires));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        _sessions.Remove(User.FindFirst(JwtRegisteredClaimNames.Jti)!.Value);
        return Ok("Du er logget ud.");
    }
}

public class LoginRequest
{
    [Required]
    public string Username { get; set; } = "";
    [Required]
    public string Password { get; set; } = "";
}

