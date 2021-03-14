using System;
using System.Collections.Generic;

namespace Melodija.Domain.Models
{
  public class ReleaseList
  {
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Title { get; set; }
    
    
    public IEnumerable<ReleaseListItem> Releases { get; set; }
  }
}