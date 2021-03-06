using System;
using System.Collections;
using AutoMapper;
using Melodija.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Melodija.api.Controllers
{
  [Route("api/artists/{artistId}/albums")]
  [ApiController]
  public class AlbumsController : ControllerBase
  {
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public AlbumsController(IRepositoryManager repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAlbumsForArtist(Guid artistId)
    {
      var artist = _repository.Artist.GetArtist(artistId,false);
      if (artist == null)
      {
        return NotFound();
      }
      else
      {
        var albums = _repository.Album.GetAlbums(artistId, false);

        return Ok(albums);
      }
    }
  }
}