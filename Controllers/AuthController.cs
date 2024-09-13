using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api_finance.DTO;
using api_finance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace api_finance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FinancedbContext _context;
        private readonly IConfiguration _config;

        public AuthController(FinancedbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // Endpoint para registrar un usuario
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRLDto userData)
        {

            var user = new Usuario
            {
                NombreUsuario = userData.NombreUsuario,
                Contrasena = BCrypt.Net.BCrypt.HashPassword(userData.Contrasena),
                Email = userData.Email,
                FechaCreacion = DateTime.Now,
                Activo = true,
                ImgUrl = Empty.ToString(),
                TemaOscuro = false,
            };

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto userData)
        {
            var user = _context.Usuarios.SingleOrDefault(u => u.Email == userData.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(userData.Contrasena, user.Contrasena))
            {
                return Unauthorized("Invalid username or password");
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email
            };
            var token = GenerateJwtToken(userDto);
            return Ok(new { user, token });
        }

        private string GenerateJwtToken(UserDto user)
        {
            IdentityModelEventSource.ShowPII = true;
            var jwt = _config.GetSection("Jwt").Get<JwtDto>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email)
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
