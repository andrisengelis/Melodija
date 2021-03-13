using System;
using System.Collections.Generic;
using Melodija.Domain;
using Melodija.Domain.Models;

namespace Melodija.Contracts
{
  public interface IReleaseRepository
  {
    IEnumerable<Release> GetReleases(Guid artistId, bool trackChanges);
    Release GetRelease(Guid artistId, Guid id, bool trackChanges);
    void CreateReleaseForArtist(Guid artistId, Release release);
    void DeleteRelease(Release release);
  }
}