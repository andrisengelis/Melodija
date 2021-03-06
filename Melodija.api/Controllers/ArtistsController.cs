using System;
using System.Linq;
using Melodija.Contracts;
using Melodija.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Melodija.api.Controllers
{
  [Route("api/artists")]
  [ApiController]
  public class ArtistsController : ControllerBase
  {
    private readonly IRepositoryManager _repository;

    public ArtistsController(IRepositoryManager repository)
    {
      _repository = repository;
    }
    
    [HttpGet]
    public IActionResult GetArtists()
    {
      try
      {
        var artists = _repository.Artist.GetAllArtists(false);

        var artistsDto = artists.Select(a => new ArtistDto
        {
          Id = a.Id,
          Name = a.Name,
          SortName = a.SortName
        }).ToList();
        
        return Ok(artistsDto);
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal server error");
      }
    }
  }
}