using System;
using System.Collections.Generic;

namespace Melodija.Domain.Models
{
  public class Artist
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SortName { get; set; }

    public IEnumerable<Release> Releases { get; set; }
  }
}