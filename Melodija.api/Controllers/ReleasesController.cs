using System;
using System.Collections.Generic;
using AutoMapper;
using Melodija.Contracts;
using Melodija.Domain;
using Melodija.Domain.DataTransferObjects;
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
  }
}