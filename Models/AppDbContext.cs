using Microsoft.EntityFrameworkCore;
namespace PetEsperanca.Models;

public class AppDbContext : DbContext{
    public DbSet<User> User {get; set;}
    public DbSet<Ong> Ong { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite("Data Source=PetDb.db");
}