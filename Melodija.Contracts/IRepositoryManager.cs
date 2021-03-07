using System;

namespace Melodija.Contracts
{
  public interface IRepositoryManager
  {
    IArtistRepository Artist { get; }
    IReleaseRepository Release { get; }
    void Save();
  }
}