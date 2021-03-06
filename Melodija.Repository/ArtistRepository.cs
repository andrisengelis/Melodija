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
  }
}