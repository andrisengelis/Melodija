using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Melodija.Contracts;
using Melodija.Data;
using Melodija.Domain;
using Melodija.Domain.Models;
using Melodija.Domain.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Melodija.Repository
{
  public class ReleaseRepository : MelodijaRepository<Release>, IReleaseRepository
  {
    public ReleaseRepository(MelodijaContext melodijaContext) : base(melodijaContext)
    {
    }

    async Task<IEnumerable<Release>> IReleaseRepository.GetReleasesAsync(Guid artistId,
      ReleaseParameters releaseParameters, bool trackChanges) =>
      await FindByCondition(a => a.ArtistId.Equals(artistId), trackChanges).OrderBy(a => a.SortTitle)
      .Skip((releaseParameters.PageNumber - 1) * releaseParameters.PageSize).Take(releaseParameters.PageSize)
      .ToListAsync();

    public Release GetRelease(Guid artistId, Guid id, bool trackChanges) =>
      FindByCondition(r => r.ArtistId.Equals(artistId) && r.Id.Equals(id), trackChanges).SingleOrDefault();

    public void CreateReleaseForArtist(Guid artistId, Release release)
    {
      release.ArtistId = artistId;
      Create(release);
    }

    public void DeleteRelease(Release release)
    {
      Delete(release);
    }
  }
}