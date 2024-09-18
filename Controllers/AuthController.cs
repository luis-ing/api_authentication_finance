using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api_finance.DTO;
using api_finance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace api_finance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly NotesDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(NotesDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // Endpoint para registrar un usuario
        [HttpPost("register")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(UserRLDto userData)
        {
            try
            {
                var userRegister = await _context.Users.SingleOrDefaultAsync(u => u.Mail == userData.Mail);

                if (userRegister != null)
                {
                    return BadRequest("El correo ya se encuentra en uso");
                }

                var userAdd = new User
                {
                    Name = userData.Name,
                    Pass = BCrypt.Net.BCrypt.HashPassword(userData.Pass),
                    Mail = userData.Mail,
                    DateCreated = DateTime.Now,
                    IsActive = true,
                };

                _context.Users.Add(userAdd);
                await _context.SaveChangesAsync();

                return Ok("Usuario registrado correctamente");
            }
            catch (System.Exception ex)
            {
                BadRequest("Error: " + ex);
                throw;
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login(LoginDto userData)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.Mail == userData.Mail);

                if (user == null || !BCrypt.Net.BCrypt.Verify(userData.Pass, user.Pass))
                {
                    return Unauthorized("Usuario o contraseña invalido");
                }
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Mail = user.Mail
                };
                var token = GenerateJwtToken(userDto);
                return Ok(new { user = userDto, token });
            }
            catch (System.Exception ex)
            {
                BadRequest("Error: " + ex);
                throw;
            }
        }

        private string GenerateJwtToken(UserDto user)
        {
            IdentityModelEventSource.ShowPII = true;
            var jwt = _config.GetSection("Jwt").Get<JwtDto>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Mail),
                new Claim("id", user.Id.ToString()),
                new Claim("mail", user.Mail)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}
