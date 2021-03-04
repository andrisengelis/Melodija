using System;
using Microsoft.AspNetCore.Mvc;

namespace Melodija.api.Controllers
{
  [Route("api/artists")]
  [ApiController]
  public class ArtistsController : ControllerBase
  {
    [HttpGet]
    public IActionResult GetArtists()
    {
      try
      {
        return Ok();
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal server error");
      }
    }
  }
}