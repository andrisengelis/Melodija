using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Melodija.Contracts;
using Melodija.Data;
using Melodija.Domain;
using Melodija.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Melodija.Repository
{
  public class ArtistRepository : MelodijaRepository<Artist>, IArtistRepository
  {
    public ArtistRepository(MelodijaContext melodijaContext) : base(melodijaContext)
    {
    }

    public async Task<IEnumerable<Artist>> GetAllArtistsAsync(bool trackChanges) =>
      await FindAll(trackChanges).OrderBy(a => a.SortName).ToListAsync();

    public async Task<Artist> GetArtistAsync(Guid artistId, bool trackChanges) =>
      await FindByCondition(a => a.Id.Equals(artistId), trackChanges).SingleOrDefaultAsync();

    public async Task<IEnumerable<Artist>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
      await FindByCondition(a => ids.Contains(a.Id), trackChanges).ToListAsync();

    public void CreateArtist(Artist artist) => Create(artist);

    public void DeleteArtist(Artist artist)
    {
      Delete(artist);
    }
  }
}