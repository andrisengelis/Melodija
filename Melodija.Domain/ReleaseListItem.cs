using System;
using System.Collections;

namespace Melodija.Domain
{
  public class ReleaseListItem
  {
    public Guid Id { get; set; }
    public Guid ReleaseId { get; set; }
    public uint Position { get; set; }
    
    public Release Release { get; set; }
    public ReleaseList ReleaseList { get; set; }
  }
}