using AutoMapper;
using ISWalksApi.Models.DTOs;
using NZWalksApi.Models.Domain;

namespace ISWalksApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();

            CreateMap<AddRegionRequestDto, Region>().ReverseMap();

            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            
            CreateMap<AddWalksRequestDto, Walk>().ReverseMap();
            
            CreateMap<Walk, WalkDto>().ReverseMap();

            CreateMap<Difficulty, DifficultyDto>().ReverseMap();

            CreateMap<UpdateWalksRequestDto, Walk>().ReverseMap();
        }
    }
}
