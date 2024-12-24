using Letrasdemusicas.Models;
using Microsoft.EntityFrameworkCore;

namespace Letrasdemusicas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Musica> Musicas { get; set; }
    }
}
