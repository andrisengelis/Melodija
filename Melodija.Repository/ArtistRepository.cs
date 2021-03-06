using System.Collections.Generic;
using System.Linq;
using Melodija.Contracts;
using Melodija.Data;
using Melodija.Domain;

namespace Melodija.Repository
{
  public class ArtistRepository : MelodijaRepository<Artist>, IArtistRepository
  {
    public ArtistRepository(MelodijaContext melodijaContext) : base(melodijaContext)
    {
    }
    
    public IEnumerable<Artist> GetAllArtists(bool trackChanges)=>
      FindAll(trackChanges).OrderBy(a => a.SortName).ToList();
  }
}