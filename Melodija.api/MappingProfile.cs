using AutoMapper;
using Melodija.Domain;

namespace Melodija.api
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Artist, ArtistDto>();
      CreateMap<Release, ReleaseDto>();
      CreateMap<ReleaseList, ReleaseListDto>();
    }
  }
}