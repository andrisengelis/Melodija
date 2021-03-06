using Melodija.Contracts;
using Melodija.Data;

namespace Melodija.Repository
{
  public class RepositoryManager : IRepositoryManager
  {
    private readonly MelodijaContext _melodijaContext;
    private ArtistRepository _artistRepository;

    public RepositoryManager(MelodijaContext melodijaContext)
    {
      _melodijaContext = melodijaContext;
    }

    public IArtistRepository Artist
    {
      get
      {
        if (_artistRepository == null)
        {
          _artistRepository = new ArtistRepository(_melodijaContext);
        }
        return _artistRepository;
      }
    }

    public void Save() => _melodijaContext.SaveChanges();
  }

}