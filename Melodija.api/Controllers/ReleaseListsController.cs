using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Melodija.Contracts;
using Melodija.Domain.DataTransferObjects;
using Melodija.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Melodija.api.Controllers
{
  [Route("api/releaselists")]
  [ApiController]
  public class ReleaseListsController : ControllerBase
  {
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public ReleaseListsController(IRepositoryManager repository, IMapper mapper, UserManager<User> userManager)
    {
      _repository = repository;
      _mapper = mapper;
      _userManager = userManager;
    }

    [HttpGet, Authorize]
    public IActionResult GetReleaseLists()
    {
      try
      {
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var releaseListsFromDb = _repository.ReleaseList.GetAllReleaseLists(false).Where(rl => rl.OwnerId == id);

        var releaseListsDto = _mapper.Map<IEnumerable<ReleaseListDto>>(releaseListsFromDb);

        return Ok(releaseListsDto);
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal server error");
      }
    }
  }
}