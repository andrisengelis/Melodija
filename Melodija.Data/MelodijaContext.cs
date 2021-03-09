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
    private DbSet<Release> Releases { get; set; }
    private DbSet<ReleaseList> ReleaseLists { get; set; }
    private DbSet<ReleaseListItem> ReleaseListItems { get; set; }
  }
}