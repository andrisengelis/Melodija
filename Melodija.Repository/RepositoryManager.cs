using Melodija.Contracts;
using Melodija.Data;

namespace Melodija.Repository
{
  public class RepositoryManager : IRepositoryManager
  {
    private readonly MelodijaContext _melodijaContext;
    private ArtistRepository _artistRepository;
    private AlbumRepository _albumRepository;

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

    public IAlbumRepository Album
    {
      get
      {
        if (_albumRepository == null)
        {
          _albumRepository = new AlbumRepository(_melodijaContext);
        }

        return _albumRepository;
      }
    }

    public void Save() => _melodijaContext.SaveChanges();
  }
}