using System.Collections.Generic;

namespace Melodija.Domain.DataTransferObjects
{
  public class ArtistForCreationDto
  {
    public string Name { get; set; }
    public string SortName { get; set; }
    
    public IEnumerable<ReleaseForCreationDto> Releases { get; set; }
  }
}