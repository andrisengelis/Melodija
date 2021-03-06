using Melodija.Contracts;
using Melodija.Data;
using Melodija.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Melodija.api.Extensions
{
  public static class ServiceExtensions
  {
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
      services.AddDbContext<MelodijaContext>(o =>
      o.UseSqlServer(configuration["Melodija:ConnectionString"]));

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
      services.AddScoped<IRepositoryManager, RepositoryManager>();

  }
}