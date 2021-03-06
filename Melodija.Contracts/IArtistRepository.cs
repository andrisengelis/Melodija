using System;
using System.Collections.Generic;
using Melodija.Domain;

namespace Melodija.Contracts
{
  public interface IArtistRepository
  {
    IEnumerable<Artist> GetAllArtists(bool trackChanges);
    Artist GetArtist(Guid artistId, bool trackChanges);
  }
}