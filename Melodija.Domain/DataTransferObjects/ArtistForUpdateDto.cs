using System.Collections.Generic;

namespace Melodija.Domain.DataTransferObjects
{
  public class ArtistForUpdateDto
  {
    public string Name { get; set; }
    public string SortName { get; set; }
    
    public IEnumerable<ReleaseForCreationDto> Releases { get; set; }
  }
}