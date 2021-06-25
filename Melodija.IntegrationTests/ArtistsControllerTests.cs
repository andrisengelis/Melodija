using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Melodija.api.Controllers;
using Melodija.Data;
using Melodija.Domain.Models;
using Melodija.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace Melodija.IntegrationTests
{
  public class ArtistsControllerTests
  {
    private readonly DbContextOptions<MelodijaContext> _contextOptions;
    private readonly ArtistsController _controller;

    public ArtistsControllerTests()
    {
      IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.Test.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
      
      var mapperConfig = new MapperConfiguration(c=>c.AddProfile(typeof(Melodija.api.MappingProfile)));
      var mapper = mapperConfig.CreateMapper();
      
      _contextOptions = new DbContextOptionsBuilder<MelodijaContext>().UseSqlServer(configuration.GetConnectionString("melodijaTest")).Options;
      var context = new MelodijaContext(_contextOptions);
      
      var repositoryManager = new RepositoryManager(context);
      _controller = new ArtistsController(repositoryManager, mapper); 
      
      Seed();
    }

    protected void Seed()
    {
      using (var context = new MelodijaContext(_contextOptions))
      {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var artist = new Artist { Name = "Radiohead",SortName = "Radiohead",Releases = new List<Release>()};
        
        context.Add(artist);
        context.SaveChanges();
      }
    }
    
    [Fact]
    public async Task GetArtists_When_Called_ReturnsOkResult()
    {
        var result = await _controller.GetArtists();
        
        Assert.IsType<OkObjectResult>(result);
    }
  }
}