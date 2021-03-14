﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Melodija.Contracts;
using Melodija.Domain;
using Melodija.Domain.Models;
using Melodija.Repository;
using Microsoft.AspNetCore.Authorization;
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
        var releaseListsFromDb = _repository.ReleaseList.GetAllReleaseLists(false);

        var releaseListsDto = _mapper.Map<IEnumerable<ReleaseList>>(releaseListsFromDb);
        
        return Ok(releaseListsDto);
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal server error");
      }  
    }
  }
}