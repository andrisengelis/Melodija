using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Melodija.Contracts;
using Melodija.Data.Migrations;
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

    public ReleaseListsController(IRepositoryManager repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpGet, Authorize]
    public IActionResult GetReleaseLists()
    {
      try
      {
        var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var releaseListsFromDb = _repository.ReleaseList.GetAllReleaseLists(false).Where(rl => rl.OwnerId == ownerId);

        var releaseListsDto = _mapper.Map<IEnumerable<ReleaseListDto>>(releaseListsFromDb);

        return Ok(releaseListsDto);
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal server error");
      }
    }

    [HttpGet("{id}", Name = "ReleaseListById"), Authorize]
    public IActionResult GetReleaseList(Guid id)
    {
      try
      {
        var releaseList = _repository.ReleaseList.GetReleaseList(id, false);
        if (releaseList == null)
        {
          return NotFound();
        }
        
        var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (releaseList.OwnerId != ownerId)
        {
          return Forbid();
        }

        var releaseListDto = _mapper.Map<ReleaseListDto>(releaseList);
        return Ok(releaseListDto);
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal server error");
      }
    }
    
    [HttpPost, Authorize]
    public IActionResult CreateReleaseList([FromBody] ReleaseListForCreateDto releaseList)
    {
      if (releaseList == null)
      {
        return BadRequest("ReleaseListForCreateDto object is null");
      }

      var releaseListEntity = _mapper.Map<ReleaseList>(releaseList);

      var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
      releaseListEntity.OwnerId = ownerId;

      _repository.ReleaseList.CreateReleaseList(releaseListEntity);
      _repository.SaveAsync();

      var releaseListToReturn = _mapper.Map<ReleaseListDto>(releaseListEntity);

      return CreatedAtRoute("ReleaseListById", new {id = releaseListToReturn.Id}, releaseListToReturn);
    }
  }
}