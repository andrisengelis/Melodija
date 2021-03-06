using System;

namespace Melodija.Contracts
{
  public interface IRepositoryManager
  {
    IArtistRepository Artist { get; }
    IAlbumRepository Album { get; }
    void Save();
  }
}