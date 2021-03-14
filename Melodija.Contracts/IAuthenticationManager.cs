using System.Threading.Tasks;
using Melodija.Domain.DataTransferObjects.Configuration;

namespace Melodija.Contracts
{
  public interface IAuthenticationManager
  {
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<string> CreateToken();
  }
}