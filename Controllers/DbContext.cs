using Microsoft.AspNetCore.Mvc;
using TaskmanagementSystem.Infrastructure.Data;
using System.Threading.Tasks;


namespace TaskmanagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DatabaseTestController : ControllerBase
    {
        private readonly AppDbContext _context;


        public DatabaseTestController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                await _context.Database.CanConnectAsync();
                return Ok("Database connection successful.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Database connection failed: {ex.Message}");
            }
        }
    }
}
