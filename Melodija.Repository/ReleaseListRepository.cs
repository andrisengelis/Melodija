using System;
using System.Collections.Generic;
using System.Linq;
using Melodija.Contracts;
using Melodija.Data;
using Melodija.Domain.Models;

namespace Melodija.Repository
{
  public class ReleaseListRepository : MelodijaRepository<ReleaseList>, IReleaseListRepository
  {
    public ReleaseListRepository(MelodijaContext melodijaContext) : base(melodijaContext)
    {
    }

    public IEnumerable<ReleaseList> GetAllReleaseLists(bool trackChanges) =>
      FindAll(trackChanges).OrderBy(rl => rl.Title).ToList();

    public ReleaseList GetReleaseList(Guid releaseListId, bool trackChanges) =>
      FindByCondition(rl => rl.Id.Equals(releaseListId), trackChanges).SingleOrDefault();

    public void CreateReleaseList(ReleaseList releaseList) => Create(releaseList);
  }
}