using System.Collections.Generic;
using Melodija.Domain;
using Melodija.Domain.Models;

namespace Melodija.Contracts
{
  public interface IReleaseListRepository
  {
    IEnumerable<ReleaseList> GetAllReleaseLists(bool trackChanges);
  }
}