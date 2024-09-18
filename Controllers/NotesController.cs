using api_finance.DTO;
using api_finance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_finance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly NotesDbContext _context;
        private readonly IConfiguration _config;

        public NotesController(NotesDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(NotesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ListNote()
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim == null)
                {
                    return Unauthorized("Token inválido");
                }
                int userId = int.Parse(userIdClaim.Value);

                var notes = _context.Notes
                .Where(u => u.UserCreated == userId)
                .Select(u => new { u.Id, u.Title, u.Content, u.DateCreated, u.UserCreated })
                .ToList();

                return Ok(notes);
            }
            catch (System.Exception ex)
            {
                BadRequest("Error: " + ex);
                throw;
            }
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNote(NoteAddDto data)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim == null)
                {
                    return Unauthorized("Token inválido");
                }
                int userId = int.Parse(userIdClaim.Value);

                var newDataNote = new Note
                {
                    Title = data.Title,
                    Content = data.Content,
                    UserCreated = userId,
                    UserUpdated = userId,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    IsActive = true,
                };

                _context.Notes.Add(newDataNote);
                await _context.SaveChangesAsync();

                return Ok("Nota creada correctamente.");
            }
            catch (System.Exception ex)
            {
                BadRequest("Error: " + ex);
                throw;
            }
        }
    }
}