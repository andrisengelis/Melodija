using System;

namespace Melodija.Domain.DataTransferObjects
{
  public class ArtistDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SortName { get; set; }
  }
}