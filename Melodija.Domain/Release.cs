using System;

namespace Melodija.Domain
{
  public class Release
  {
    public Guid Id { get; set; }
    public Guid ArtistId { get; set; }
    public string Title { get; set; }
    public string SortTitle { get; set; }
    
    public Artist Artist { get; set; }
  }
}