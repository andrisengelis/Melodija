using System;
using System.Collections.Generic;
using Melodija.Domain;
using Melodija.Domain.Models;

namespace Melodija.Contracts
{
  public interface IArtistRepository
  {
    IEnumerable<Artist> GetAllArtists(bool trackChanges);
    Artist GetArtist(Guid artistId, bool trackChanges);
    IEnumerable<Artist> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    void CreateArtist(Artist artist);
    void DeleteArtist(Artist artist);
  }
}