using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Melodija.Domain;
using Melodija.Domain.Models;
using Melodija.Domain.RequestFeatures;

namespace Melodija.Contracts
{
  public interface IReleaseRepository
  {
    Task<IEnumerable<Release>> GetReleasesAsync(Guid artistId, ReleaseParameters releaseParameters, bool trackChanges);
    Release GetRelease(Guid artistId, Guid id, bool trackChanges);
    void CreateReleaseForArtist(Guid artistId, Release release);
    void DeleteRelease(Release release);
  }
}