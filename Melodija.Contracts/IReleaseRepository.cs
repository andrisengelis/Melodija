using System;
using System.Collections.Generic;
using Melodija.Domain;

namespace Melodija.Contracts
{
  public interface IReleaseRepository
  {
    IEnumerable<Release> GetReleases(Guid artistId, bool trackChanges);
  }
}