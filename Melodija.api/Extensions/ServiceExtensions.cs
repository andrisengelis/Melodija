using Melodija.Contracts;
using Melodija.Data;
using Melodija.Domain.Models;
using Melodija.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Melodija.api.Extensions
{
  public static class ServiceExtensions
  {
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
      services.AddDbContext<MelodijaContext>(o =>
        o.UseSqlServer(configuration.GetConnectionString("melodijaSql")));

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
      services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureCors(this IServiceCollection services) =>
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy", builder =>
          builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
      });

    public static void ConfigureIdentity(this IServiceCollection services)
    {
      var builder = services.AddIdentityCore<User>(o =>
      {
        o.Password.RequireDigit = true;
        o.Password.RequireLowercase = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 6;
        o.User.RequireUniqueEmail = true;
      });

      builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
      builder.AddEntityFrameworkStores<MelodijaContext>().AddDefaultTokenProviders();
    }
  }
}