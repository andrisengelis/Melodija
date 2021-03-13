using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Melodija.Contracts;
using Melodija.Domain;
using Melodija.Domain.DataTransferObjects;
using Melodija.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Melodija.api.Controllers
{
  [Route("api/artists")]
  [ApiController]
  public class ArtistsController : ControllerBase
  {
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public ArtistsController(IRepositoryManager repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetArtists()
    {
      try
      {
        var artists = _repository.Artist.GetAllArtists(false);

        var artistsDto = _mapper.Map<IEnumerable<ArtistDto>>(artists);

        return Ok(artistsDto);
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal server error");
      }
    }

    [HttpGet("{id}", Name = "ArtistById")]
    public IActionResult GetArtist(Guid id)
    {
      try
      {
        var artist = _repository.Artist.GetArtist(id, false);
        if (artist == null)
        {
          return NotFound();
        }
        else
        {
          var artistDto = _mapper.Map<ArtistDto>(artist);
          return Ok(artistDto);
        }
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal server error");
      }
    }

    [HttpGet("collection/({ids})", Name = "ArtistCollection")]
    public IActionResult GetArtistCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]
      IEnumerable<Guid> ids)
    {
      if (ids == null)
      {
        return BadRequest("Parameter ids is null");
      }

      var artistEntities = _repository.Artist.GetByIds(ids, false);

      if (ids.Count() != artistEntities.Count())
      {
        return NotFound();
      }

      var artistsToReturn = _mapper.Map<IEnumerable<ArtistDto>>(artistEntities);
      return Ok(artistsToReturn);
    }

    [HttpPost()]
    public IActionResult CreateArtist([FromBody] ArtistForCreationDto artist)
    {
      if (artist == null)
      {
        return BadRequest("Artist object is null");
      }

      var artistEntity = _mapper.Map<Artist>(artist);
      _repository.Artist.CreateArtist(artistEntity);
      _repository.Save();

      var artistToReturn = _mapper.Map<ArtistDto>(artistEntity);

      return CreatedAtRoute("ArtistById", new {id = artistToReturn.Id}, artistToReturn);
    }

    [HttpPost("collection")]
    public IActionResult CreateArtistCollection([FromBody] IEnumerable<ArtistForCreationDto> artistCollection)
    {
      if (artistCollection == null)
      {
        return BadRequest("Artist collection is null");
      }

      var artistEntities = _mapper.Map<IEnumerable<Artist>>(artistCollection);

      foreach (var artist in artistEntities)
      {
        _repository.Artist.CreateArtist(artist);
      }

      _repository.Save();

      var artistCollectionToReturn = _mapper.Map<IEnumerable<ArtistDto>>(artistEntities);
      var ids = string.Join(",", artistCollectionToReturn.Select(a => a.Id));

      return CreatedAtRoute("ArtistCollection", new {ids}, artistCollectionToReturn);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteArtist(Guid id)
    {
      var artist = _repository.Artist.GetArtist(id, false);

      if (artist == null)
      {
        return NotFound();
      }

      _repository.Artist.DeleteArtist(artist);
      _repository.Save();

      return NoContent();
    }
  }
}