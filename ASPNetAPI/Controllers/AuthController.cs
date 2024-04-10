using ASPNetAPI.Filters;
using ASPNetAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASPNetAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [Route("token")]
    public IActionResult GetToken(SignInForm form)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new(ClaimTypes.Email, form.Email),
                    new(ClaimTypes.Name, form.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(tokenHandler.WriteToken(token));
            }
            catch
            {
                return Unauthorized();
            }
        }

        return Unauthorized();
    }
}
