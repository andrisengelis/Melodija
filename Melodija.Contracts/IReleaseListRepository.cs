using System.Collections.Generic;
using Melodija.Domain;

namespace Melodija.Contracts
{
  public interface IReleaseListRepository
  {
    IEnumerable<ReleaseList> GetAllReleaseLists(bool trackChanges);
  }
}