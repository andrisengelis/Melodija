using System;
using System.Collections.Generic;

namespace Melodija.Domain
{
  public class Artist
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SortName { get; set; }

    private IEnumerable<Release> Releases { get; set; }
  }
}