using Microsoft.EntityFrameworkCore;
namespace PetEsperanca.Models
{
    public class AppDbContext : DbContext{
    public DbSet<User> User {get; set;}
    public DbSet<Ong> Ong { get; set; }
    public DbSet<Evento> Evento { get; set; }
    public DbSet<Comentario> Comment { get; set; }
    public DbSet<Voluntario> Voluntario { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite("Data Source=PetDb.db");
    }
}

