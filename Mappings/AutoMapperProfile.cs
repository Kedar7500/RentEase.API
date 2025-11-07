using AutoMapper;
using RentEase.API.Models.Domain;
using RentEase.API.Models.DTOs;

namespace RentEase.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Property, PropertyDto>().ReverseMap();
            CreateMap<Property, AddPropertyDto>().ReverseMap();
            CreateMap<Property, UpdatePropertyDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
        }
    }
}
