using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Melodija.Contracts;
using Melodija.Domain.DataTransferObjects;
using Melodija.Domain.Models;
using Melodija.Domain.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Melodija.api.Controllers
{
  [Route("api/artists/{artistId}/releases")]
  [ApiController]
  public class ReleasesController : ControllerBase
  {
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public ReleasesController(IRepositoryManager repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetReleasesForArtist(Guid artistId, [FromQuery] ReleaseParameters releaseParameters)
    {
      var artist = await _repository.Artist.GetArtistAsync(artistId, false);

      if (artist == null)
      {
        return NotFound();
      }
      else
      {
        var releasesFromDb = await _repository.Release.GetReleasesAsync(artistId, releaseParameters, false);

        var releasesDto = _mapper.Map<IEnumerable<ReleaseDto>>(releasesFromDb);

        return Ok(releasesDto);
      }
    }

    [HttpGet("{id}", Name = "GetReleaseForArtist")]
    public async Task<IActionResult> GetReleaseForArtist(Guid artistId, Guid id)
    {
      var artist = await _repository.Artist.GetArtistAsync(artistId, false);
      if (artist == null)
      {
        return NotFound();
      }

      var releaseDb = _repository.Release.GetRelease(artistId, id, false);
      if (releaseDb == null)
      {
        return NotFound();
      }

      var release = _mapper.Map<ReleaseDto>(releaseDb);
      return Ok(release);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReleaseForArtist(Guid artistId, [FromBody] ReleaseForCreationDto release)
    {
      if (release == null)
      {
        return BadRequest("ReleaseForCreationDto object is null");
      }

      var artist = await _repository.Artist.GetArtistAsync(artistId, false);
      if (artist == null)
      {
        return NotFound();
      }

      var releaseEntity = _mapper.Map<Release>(release);

      _repository.Release.CreateReleaseForArtist(artistId, releaseEntity);
      await _repository.SaveAsync();

      var releaseToReturn = _mapper.Map<ReleaseDto>(releaseEntity);
      return CreatedAtRoute("GetReleaseForArtist", new {artistId, id = releaseToReturn.Id}, releaseToReturn);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReleaseForArtist(Guid artistId, Guid id)
    {
      var artist = await _repository.Artist.GetArtistAsync(artistId, false);
      if (artist == null)
      {
        return NotFound();
      }

      var releaseForArtist = _repository.Release.GetRelease(artistId, id, false);
      if (releaseForArtist == null)
      {
        return NotFound();
      }

      _repository.Release.DeleteRelease(releaseForArtist);
      await _repository.SaveAsync();

      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReleaseForArtist(Guid artistId, Guid id, [FromBody] ReleaseForUpdateDto release)
    {
      if (release == null)
      {
        return BadRequest("ReleaseForUnpdateDto object is null");
      }

      var artist = await _repository.Artist.GetArtistAsync(artistId, false);

      if (artist == null)
      {
        return NotFound();
      }

      var releaseEntity = _repository.Release.GetRelease(artistId, id, true);
      if (releaseEntity == null)
      {
        return NotFound();
      }

      _mapper.Map(release, releaseEntity);
      await _repository.SaveAsync();

      return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PartiallyUpdateReleaseForArtist(Guid artistId, Guid id,
      [FromBody] JsonPatchDocument<ReleaseForUpdateDto> patchDoc)
    {
      if (patchDoc == null)
      {
        return BadRequest("patchDoc object is null");
      }

      var artist = await _repository.Artist.GetArtistAsync(artistId, false);

      if (artist == null)
      {
        return NotFound();
      }

      var releaseEntity = _repository.Release.GetRelease(artistId, id, true);
      if (releaseEntity == null)
      {
        return NotFound();
      }

      var releaseEntityToPatch = _mapper.Map<ReleaseForUpdateDto>(releaseEntity);
      patchDoc.ApplyTo(releaseEntityToPatch);

      _mapper.Map(releaseEntityToPatch, releaseEntity);
      
      await _repository.SaveAsync();
      return NoContent();
    }
  }
}