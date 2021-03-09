using System;
using AutoMapper;
using Melodija.Contracts;
using Melodija.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Melodija.api.Controllers
{
  public class ReleaseListsController : ControllerBase 
  {
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public ReleaseListsController(IRepositoryManager repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult GetReleaseLists()
    {
      try
      {
        var releaseLists = _repository.ReleaseList.GetAllReleaseLists(false);
        
        return Ok(releaseLists);
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal server error");
      }  
    }
  }
}