using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Melodija.Contracts;
using Melodija.Data;
using Melodija.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Melodija.Repository
{
  public class ReleaseListRepository : MelodijaRepository<ReleaseList>, IReleaseListRepository
  {
    public ReleaseListRepository(MelodijaContext melodijaContext) : base(melodijaContext)
    {
    }

    public async Task<IEnumerable<ReleaseList>> GetAllReleaseListsByOwnerIdAsync(Guid ownerId, bool trackChanges) =>
      await FindByCondition(rl => rl.OwnerId.Equals(ownerId), trackChanges).OrderBy(rl => rl.Title).ToListAsync();

    public async Task<ReleaseList> GetReleaseListAsync(Guid releaseListId, bool trackChanges) =>
      await FindByCondition(rl => rl.Id.Equals(releaseListId), trackChanges).SingleOrDefaultAsync();

    public void CreateReleaseList(ReleaseList releaseList) => Create(releaseList);
  }
}