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
    public async Task GetArtists_ReturnsOkObjectResult_WithAListOfArtists()
    {
      var mapperConfig = new MapperConfiguration(c=>c.AddProfile(typeof(api.MappingProfile)));
      var mapper = mapperConfig.CreateMapper();
      
      var mockRepo = new Mock<IRepositoryManager>();
      mockRepo.Setup(repo => repo.Artist.GetAllArtistsAsync(false)).ReturnsAsync(GetAllTestArtists());
      var sut = new ArtistsController(mockRepo.Object, mapper);

      var result = await sut.GetArtists();

      var okResult = Assert.IsType<OkObjectResult>(result);
      var artistDtos = Assert.IsAssignableFrom<IEnumerable<ArtistDto>>(okResult.Value);
      Assert.Equal(2,artistDtos.Count());
    }

    [Fact]
    public async Task CreateArtist_ReturnsBadRequestObjectResult_WhenModelStateIsInvalid()
    {
      var mapperConfig = new MapperConfiguration(c=>c.AddProfile(typeof(api.MappingProfile)));
      var mapper = mapperConfig.CreateMapper();
      
      var mockRepo = new Mock<IRepositoryManager>();
      mockRepo.Setup(repo => repo.Artist.GetAllArtistsAsync(false)).ReturnsAsync(GetAllTestArtists());
      var sut = new ArtistsController(mockRepo.Object, mapper);
      
      var result = await sut.CreateArtist(null);

      var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
      Assert.Equal("Artist object is null", badRequestObjectResult.Value);
    }

    [Fact]
    public async Task CreateArtist_ReturnsACreatedAtRouteAndAddArtist_WhenModelIsStateValid()
    {
      var mapperConfig = new MapperConfiguration(c=>c.AddProfile(typeof(api.MappingProfile)));
      var mapper = mapperConfig.CreateMapper();
      
      var mockRepo = new Mock<IRepositoryManager>();
      mockRepo.Setup(repo => repo.Artist.CreateArtist(It.IsAny<Artist>())).Verifiable();
        
      var sut = new ArtistsController(mockRepo.Object, mapper);

      var newArtist = new ArtistForCreationDto
      {
        Name = "Radiohead",
        SortName = "Radiohead",
        Releases =  new List<ReleaseForCreationDto>()
      };

      var result = await sut.CreateArtist(newArtist);

      var createdAtRouteObjectResult = Assert.IsType<CreatedAtRouteResult>(result);
      Assert.Equal("ArtistById", createdAtRouteObjectResult.RouteName);
      mockRepo.Verify();
    }

    [Fact]
    public async Task GetArtist_ReturnsANotFoundObjectResult_WhenIdDoesNotExist()
    {
      var mapperConfig = new MapperConfiguration(c=>c.AddProfile(typeof(api.MappingProfile)));
      var mapper = mapperConfig.CreateMapper();
      
      var mockRepo = new Mock<IRepositoryManager>();
      mockRepo.Setup(repo => repo.Artist.GetArtistAsync(It.IsAny<Guid>(),false)).ReturnsAsync((Artist)null);

      var sut = new ArtistsController(mockRepo.Object, mapper);

      var result = await sut.GetArtist(new Guid());

      Assert.IsType<NotFoundResult>(result);
      }

    [Fact]
    public async Task GetArtist_ReturnsOkObjectResultWithArtist_WhenIdExists()
    {
      var mapperConfig = new MapperConfiguration(c=>c.AddProfile(typeof(api.MappingProfile)));
      var mapper = mapperConfig.CreateMapper();
      
      var testArtistId = new Guid("FF618503-9981-461E-980C-DFE9E34E1DEB");
      
      var mockRepo = new Mock<IRepositoryManager>();
      mockRepo.Setup(repo => repo.Artist.GetArtistAsync(testArtistId,false)).ReturnsAsync(GetTestArtist());

      var sut = new ArtistsController(mockRepo.Object, mapper);
      
      var result = await sut.GetArtist(testArtistId);
      
      var okResult = Assert.IsType<OkObjectResult>(result);
      var artistDto = Assert.IsAssignableFrom<ArtistDto>(okResult.Value);
      Assert.Equal("Wolf Alice", artistDto.Name);
      Assert.Equal("Wolf Alice", artistDto.SortName);
      Assert.Equal(testArtistId, artistDto.Id);
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

    private Artist GetTestArtist()
    {
      return new()
      {
        Id = new Guid("FF618503-9981-461E-980C-DFE9E34E1DEB"),
        Name = "Wolf Alice",
        SortName = "Wolf Alice",
        Releases = new List<Release>()
      };
    }
  }
}