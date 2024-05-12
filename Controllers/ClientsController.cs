using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]

public class ClientsController : ControllerBase
{
     private readonly ApplicationDbContext _context;
    public ClientsController(ApplicationDbContext context)
    {
        _context = context;
    }

    //GET all clients
    [HttpGet]
    public async Task<ActionResult<List<Client>>> GetAll()
    {
        var clients = await _context.Clients.ToListAsync();
        return Ok(clients);
    }

}