using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Melodija.Contracts;
using Melodija.Data;
using Melodija.Domain;

namespace Melodija.Repository
{
  public class AlbumRepository : MelodijaRepository<Album>, IAlbumRepository
  {
    public AlbumRepository(MelodijaContext melodijaContext) : base(melodijaContext)
    {
    }

    IEnumerable<Album> IAlbumRepository.GetAlbums(Guid artistId, bool trackChanges) =>
      FindByCondition(a => a.ArtistId.Equals(artistId), trackChanges).OrderBy(a => a.SortTitle);
  }
}