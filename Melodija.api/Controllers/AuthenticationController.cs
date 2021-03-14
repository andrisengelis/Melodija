using System.Threading.Tasks;
using AutoMapper;
using Melodija.Contracts;
using Melodija.Domain.DataTransferObjects.Configuration;
using Melodija.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Melodija.api.Controllers
{
  [Route("api/authentication")]
  [ApiController]
  public class AuthenticationController : ControllerBase
  {
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IAuthenticationManager _authManager;

    public AuthenticationController(UserManager<User> userManager, IMapper mapper, IAuthenticationManager authManager)
    {
      _userManager = userManager;
      _mapper = mapper;
      _authManager = authManager;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
    {
      var user = _mapper.Map<User>(userForRegistrationDto);

      var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);
      if (!result.Succeeded)
      {
        foreach (var error in result.Errors)
        {
          ModelState.TryAddModelError(error.Code, error.Description);
        }

        return BadRequest(ModelState);
      }

      await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);
      return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthenticationDto)
    {
      if (!await _authManager.ValidateUser(userForAuthenticationDto))
      {
        return Unauthorized();
      }

      return Ok(new {Token = await _authManager.CreateToken()});
    }
  }
}