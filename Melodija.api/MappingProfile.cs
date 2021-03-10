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
      CreateMap<ArtistForCreationDto, Artist>();
      CreateMap<Release, ReleaseDto>();
      CreateMap<ReleaseForCreationDto, Release>();
      CreateMap<ReleaseList, ReleaseListDto>();
    }
  }
}