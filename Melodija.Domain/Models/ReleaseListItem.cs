using System;

namespace Melodija.Domain.Models
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