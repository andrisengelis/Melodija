using System;
using System.Collections.Generic;
using AutoMapper;
using Melodija.Contracts;
using Melodija.Domain;
using Melodija.Domain.DataTransferObjects;
using Melodija.Domain.Models;
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
    public IActionResult GetReleasesForArtist(Guid artistId)
    {
      var artist = _repository.Artist.GetArtist(artistId,false);
      
      if (artist == null)
      {
        return NotFound();
      }
      else
      {
        var releasesFromDb = _repository.Release.GetReleases(artistId, false);

        var releasesDto = _mapper.Map<IEnumerable<ReleaseDto>>(releasesFromDb);

        return Ok(releasesDto);
      }
    }

    [HttpGet("{id}", Name="GetReleaseForArtist")]
    public IActionResult GetReleaseForArtist(Guid artistId, Guid id)
    {
      var artist = _repository.Artist.GetArtist(artistId, false);
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
    public IActionResult CreateReleaseForArtist(Guid artistId, [FromBody] ReleaseForCreationDto release)
    {
      if (release == null)
      {
        return BadRequest("ReleaseForCreationDto object is null");
      }
      
      var artist = _repository.Artist.GetArtist(artistId, false);
      if (artist == null)
      {
        return NotFound();
      }

      var releaseEntity = _mapper.Map<Release>(release);
      
      _repository.Release.CreateReleaseForArtist(artistId, releaseEntity);
      _repository.Save();

      var releaseToReturn = _mapper.Map<ReleaseDto>(releaseEntity);
      return CreatedAtRoute("GetReleaseForArtist", new {artistId, id = releaseToReturn.Id}, releaseToReturn);
    }
  }
}