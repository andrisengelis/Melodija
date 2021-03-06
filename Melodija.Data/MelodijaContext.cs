using Melodija.Domain;
using Microsoft.EntityFrameworkCore;

namespace Melodija.Data
{
  public class MelodijaContext : DbContext
  {
    public MelodijaContext(DbContextOptions options) : base(options)
    {
      
    }

    private DbSet<Artist> Artists { get; set; }
    private DbSet<Album> Albums { get; set; }
  }
}