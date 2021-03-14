using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public async Task<IActionResult> GetArtists()
    {
      try
      {
        var artists = await _repository.Artist.GetAllArtistsAsync(false);

        var artistsDto = _mapper.Map<IEnumerable<ArtistDto>>(artists);

        return Ok(artistsDto);
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal server error");
      }
    }

    [HttpGet("{id}", Name = "ArtistById")]
    public async Task<IActionResult> GetArtist(Guid id)
    {
      try
      {
        var artist = await _repository.Artist.GetArtistAsync(id, false);
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
    public async Task<IActionResult> GetArtistCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]
      IEnumerable<Guid> ids)
    {
      if (ids == null)
      {
        return BadRequest("Parameter ids is null");
      }

      var artistEntities = await _repository.Artist.GetByIdsAsync(ids, false);

      if (ids.Count() != artistEntities.Count())
      {
        return NotFound();
      }

      var artistsToReturn = _mapper.Map<IEnumerable<ArtistDto>>(artistEntities);
      return Ok(artistsToReturn);
    }

    [HttpPost()]
    public async Task<IActionResult> CreateArtist([FromBody] ArtistForCreationDto artist)
    {
      if (artist == null)
      {
        return BadRequest("Artist object is null");
      }

      var artistEntity = _mapper.Map<Artist>(artist);
      _repository.Artist.CreateArtist(artistEntity);
      await _repository.SaveAsync();

      var artistToReturn = _mapper.Map<ArtistDto>(artistEntity);

      return CreatedAtRoute("ArtistById", new {id = artistToReturn.Id}, artistToReturn);
    }

    [HttpPost("collection")]
    public async Task<IActionResult> CreateArtistCollection(
      [FromBody] IEnumerable<ArtistForCreationDto> artistCollection)
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

      await _repository.SaveAsync();

      var artistCollectionToReturn = _mapper.Map<IEnumerable<ArtistDto>>(artistEntities);
      var ids = string.Join(",", artistCollectionToReturn.Select(a => a.Id));

      return CreatedAtRoute("ArtistCollection", new {ids}, artistCollectionToReturn);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArtist(Guid id)
    {
      var artist = await _repository.Artist.GetArtistAsync(id, false);

      if (artist == null)
      {
        return NotFound();
      }

      _repository.Artist.DeleteArtist(artist);
      await _repository.SaveAsync();

      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArtist(Guid id, [FromBody] ArtistForUpdateDto artist)
    {
      if (artist == null)
      {
        return BadRequest("ArtistForUpdateDto object is null");
      }

      var artistEntity = await _repository.Artist.GetArtistAsync(id, true);

      if (artistEntity == null)
      {
        return NotFound();
      }

      _mapper.Map(artist, artistEntity);
      await _repository.SaveAsync();

      return NoContent();
    }
  }
}