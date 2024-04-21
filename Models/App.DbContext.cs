using Microsoft.EntityFrameworkCore;
namespace PetEsperanca.Models;

public class AppDbContext : DbContext{
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite("Data Source=PetDb.db");
}