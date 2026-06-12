using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalksAPI.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Domain to DTO
            CreateMap<Region, RegionDto>().ReverseMap();

            // CreateRegionRequestDto to Domain
            CreateMap<CreateRegionRequestDto, Region>().ReverseMap();

            // UpdateRegionRequestDto to Domain
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        }
    }
}
