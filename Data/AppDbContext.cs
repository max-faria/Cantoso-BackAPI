using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Data{
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
            .HasOne(c => c.Address)
            .WithOne(e => e.Client)
            .HasForeignKey<Client>(c => c.AddressId);
        }
    public DbSet<Pizza> Pizzas {get; set; }
    public DbSet<Client> Clients {get; set; }
    public DbSet<Address> Addresses {get; set; }
    public DbSet<User> Users {get; set; }
}
}