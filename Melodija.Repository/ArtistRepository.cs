using System;
using System.Collections.Generic;
using System.Linq;
using Melodija.Contracts;
using Melodija.Data;
using Melodija.Domain;
using Melodija.Domain.Models;

namespace Melodija.Repository
{
  public class ArtistRepository : MelodijaRepository<Artist>, IArtistRepository
  {
    public ArtistRepository(MelodijaContext melodijaContext) : base(melodijaContext)
    {
    }
    
    public IEnumerable<Artist> GetAllArtists(bool trackChanges)=>
      FindAll(trackChanges).OrderBy(a => a.SortName).ToList();

    public Artist GetArtist(Guid artistId, bool trackChanges) =>
      FindByCondition(a => a.Id.Equals(artistId), trackChanges).SingleOrDefault();

    public IEnumerable<Artist> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
      FindByCondition(a => ids.Contains(a.Id), trackChanges).ToList();

    public void CreateArtist(Artist artist) => Create(artist);
  }
}