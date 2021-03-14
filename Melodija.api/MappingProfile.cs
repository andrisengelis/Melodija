using AutoMapper;
using Melodija.Domain;
using Melodija.Domain.DataTransferObjects;
using Melodija.Domain.DataTransferObjects.Configuration;
using Melodija.Domain.Models;

namespace Melodija.api
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Artist, ArtistDto>();
      CreateMap<ArtistForCreationDto, Artist>();
      CreateMap<ArtistForUpdateDto, Artist>();
      CreateMap<Release, ReleaseDto>();
      CreateMap<ReleaseForCreationDto, Release>();
      CreateMap<ReleaseForUpdateDto, Release>().ReverseMap();
      CreateMap<ReleaseList, ReleaseListDto>();
      CreateMap<ReleaseListForCreateDto, ReleaseList>();

      CreateMap<UserForRegistrationDto, User>();
    }
  }
}