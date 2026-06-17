using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.DTOs;
using NZWalksAPI.Models.DTOs;

namespace NZWalksAPI.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Domain to DTO
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<CreateRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }
}
