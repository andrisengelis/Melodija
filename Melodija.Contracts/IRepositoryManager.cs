using System;
using System.Threading.Tasks;

namespace Melodija.Contracts
{
  public interface IRepositoryManager
  {
    IArtistRepository Artist { get; }
    IReleaseRepository Release { get; }
    IReleaseListRepository ReleaseList { get; }
    Task SaveAsync();
  }
}