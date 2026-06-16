using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
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

            // Domain to DTO
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();

            // Domain to DTO
            CreateMap<Walk, WalkDto>().ReverseMap();
        }
    }
}
