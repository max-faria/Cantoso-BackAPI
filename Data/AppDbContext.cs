using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Data{
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Pizza> Pizzas {get; set; }
}
}