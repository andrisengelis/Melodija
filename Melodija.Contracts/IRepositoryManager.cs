using System;

namespace Melodija.Contracts
{
  public interface IRepositoryManager
  {
    IArtistRepository Artist { get; }
    void Save();
  }
}