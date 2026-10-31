using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ReencarnacionExpress.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Enemigo> Enemigos { get; set; }
        public DbSet<Historia> Historias { get; set; }
        public DbSet<Personaje> Personajes { get; set; }
        public DbSet<Region> Regiones { get; set; }
        public DbSet<Vestigios> Vestigios { get; set; }
        public DbSet<Medio> Medios { get; set; }
    }
}