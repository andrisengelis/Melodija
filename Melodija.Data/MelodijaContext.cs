using Melodija.Domain;
using Melodija.Domain.Models;
using Melodija.Domain.Models.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Melodija.Data
{
  public class MelodijaContext : IdentityDbContext<User>
  {
    public MelodijaContext(DbContextOptions options) : base(options)
    {
      
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.ApplyConfiguration(new RoleConfiguration());
    }

    private DbSet<Artist> Artists { get; set; }
    private DbSet<Release> Releases { get; set; }
    private DbSet<ReleaseList> ReleaseLists { get; set; }
    private DbSet<ReleaseListItem> ReleaseListItems { get; set; }
  }
}