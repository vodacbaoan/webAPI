using Business.Commands.Auth;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public AuthController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var command = new LoginCommand(model.Username, model.Password);
            var user = await _mediator.Send(command);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            var jwtToken = GenerateJwtToken(user);
            return Ok(new { Token = jwtToken });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var command = new RegisterCommand(model.Username, model.Password);
            var createdUser = await _mediator.Send(command);

            
            return CreatedAtAction(nameof(Login), new { username = createdUser.Username }, createdUser);
        }

        private string GenerateJwtToken(User user)
        {
           
            var secretKey = _configuration["JwtSettings:Secret"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

         
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

         
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username), 
                    new Claim(ClaimTypes.Role, user.Role ?? string.Empty) 
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiresInMinutes"])),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [HttpGet("check")]
        public IActionResult Check()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new { message = "User is authenticated." });
            }
            return Unauthorized(new { message = "User is not authenticated." });
        }
        [HttpGet("role")]
        public IActionResult CheckUserRole()
        {
            if (User.Identity.IsAuthenticated)
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name);
                var roleClaim = User.FindFirst(ClaimTypes.Role);

                var username = usernameClaim != null ? usernameClaim.Value : "Unknown";
                var userRole = roleClaim != null ? roleClaim.Value : "No role assigned";

                return Ok(new { Username = username, Role = userRole });
            }
            return Unauthorized(new { message = "User is not authenticated." });
        }

    }
}
