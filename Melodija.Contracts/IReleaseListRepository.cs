using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Melodija.Domain;
using Melodija.Domain.Models;

namespace Melodija.Contracts
{
  public interface IReleaseListRepository
  {
    Task<IEnumerable<ReleaseList>> GetAllReleaseListsByOwnerIdAsync(Guid ownerId, bool trackChanges);
    Task<ReleaseList> GetReleaseListAsync(Guid releaseListId, bool trackChanges);
    void CreateReleaseList(ReleaseList releaseList);
  }
}