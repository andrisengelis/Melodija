using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Melodija.api.Controllers;
using Melodija.Contracts;
using Melodija.Domain.DataTransferObjects;
using Melodija.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Melodija.Tests
{
  public class ArtistsControllerTests
  {
    
    [Fact]
    public async Task Test1()
    {
      var mapperConfig = new MapperConfiguration(c=>c.AddProfile(typeof(api.MappingProfile)));
      var mapper = mapperConfig.CreateMapper();
      
      var mockRepo = new Mock<IRepositoryManager>();
      mockRepo.Setup(repo => repo.Artist.GetAllArtistsAsync(false)).ReturnsAsync(GetAllTestArtists());
      var sut = new ArtistsController(mockRepo.Object, mapper);

      var result = await sut.GetArtists();

      var objectResult = Assert.IsType<OkObjectResult>(result);
      var artistDtos = Assert.IsAssignableFrom<IEnumerable<ArtistDto>>(objectResult.Value);
      Assert.Equal(2,artistDtos.Count());
    }

    private IEnumerable<Artist> GetAllTestArtists()
    {
      var artists = new List<Artist>
      {
        new() {Id = new Guid(), Name = "Modest Mouse", SortName = "Modest Mouse", Releases = new List<Release>()},
        new() {Id = new Guid(), Name = "John Grant", SortName = "Grant, John", Releases = new List<Release>()}
      };

      return artists;
    }
  }
}