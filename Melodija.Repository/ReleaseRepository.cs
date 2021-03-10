using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Melodija.Contracts;
using Melodija.Data;
using Melodija.Domain;
using Melodija.Domain.Models;

namespace Melodija.Repository
{
  public class ReleaseRepository : MelodijaRepository<Release>, IReleaseRepository
  {
    public ReleaseRepository(MelodijaContext melodijaContext) : base(melodijaContext)
    {
    }

    IEnumerable<Release> IReleaseRepository.GetReleases(Guid artistId, bool trackChanges) =>
      FindByCondition(a => a.ArtistId.Equals(artistId), trackChanges).OrderBy(a => a.SortTitle);
  }
}