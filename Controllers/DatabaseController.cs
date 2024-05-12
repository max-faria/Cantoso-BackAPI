using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoPizza.Data;

[ApiController]
[Route("[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DatabaseController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("test-connection")]
    public async Task<IActionResult> TestConnection()
    {
        try
        {
            if (await _context.Database.CanConnectAsync())
            {
                return Ok("Database connected successfully.");
            }
            else
            {
                return Problem("Database connection failed, but no exception was thrown.");
            }
        }
        catch (Exception ex)
        {
            return Problem("Failed to connect to the database: " + ex.Message);
        }
    }
}
