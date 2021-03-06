using System;
using System.Collections.Generic;
using AutoMapper;
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
  }
}