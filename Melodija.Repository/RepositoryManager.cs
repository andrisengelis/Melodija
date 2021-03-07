using Melodija.Contracts;
using Melodija.Data;

namespace Melodija.Repository
{
  public class RepositoryManager : IRepositoryManager
  {
    private readonly MelodijaContext _melodijaContext;
    private ArtistRepository _artistRepository;
    private ReleaseRepository _releaseRepository;

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

    public IReleaseRepository Release
    {
      get
      {
        if (_releaseRepository == null)
        {
          _releaseRepository = new ReleaseRepository(_melodijaContext);
        }

        return _releaseRepository;
      }
    }

    public void Save() => _melodijaContext.SaveChanges();
  }
}