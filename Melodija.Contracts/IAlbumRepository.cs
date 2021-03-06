using System;
using System.Collections.Generic;
using Melodija.Domain;

namespace Melodija.Contracts
{
  public interface IAlbumRepository
  {
    IEnumerable<Album> GetAlbums(Guid artistId, bool trackChanges);
  }
}