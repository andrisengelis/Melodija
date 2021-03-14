using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace Melodija.Domain.DataTransferObjects.Configuration
{
  public class UserForAuthenticationDto
  {
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
  }
}