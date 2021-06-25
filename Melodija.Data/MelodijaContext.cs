using Melodija.Domain;
using Melodija.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Melodija.Data
{
  public class MelodijaContext : IdentityDbContext<User>
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