using System;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Melodija.Contracts;
using Melodija.Domain;
using Melodija.Domain.DataTransferObjects;
using Melodija.Domain.DataTransferObjects.Configuration;
using Melodija.Domain.Models;
using Melodija.Repository;
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
        // TODO: Work on individual release lists
        //var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var releaseListsFromDb = _repository.ReleaseList.GetAllReleaseLists(false);

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