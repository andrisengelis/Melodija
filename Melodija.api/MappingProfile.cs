using AutoMapper;
using Melodija.Domain;
using Melodija.Domain.DataTransferObjects;
using Melodija.Domain.Models;

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