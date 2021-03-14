using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Melodija.Domain;
using Melodija.Domain.Models;

namespace Melodija.Contracts
{
  public interface IArtistRepository
  {
    Task<IEnumerable<Artist>> GetAllArtistsAsync(bool trackChanges);
    Task<Artist> GetArtistAsync(Guid artistId, bool trackChanges);
    Task<IEnumerable<Artist>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    void CreateArtist(Artist artist);
    void DeleteArtist(Artist artist);
  }
}