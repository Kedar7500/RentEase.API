using AutoMapper;
using RentEase.API.Models.Domain;
using RentEase.API.Models.DTOs;
using RentEase.API.Repositories.Interfaces;
using RentEase.API.Services.Interfaces;

namespace RentEase.API.Services.Implementations
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyInterface propertyRepository;
        private readonly IMapper mapper;

        public PropertyService(IPropertyInterface propertyRepository, IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
        }

        public async Task<List<Property>> GetAllProperties()
        {
            return await propertyRepository.GetAllProperty();
        }
        public Task<Property> GetPropertyById(int id)
        {
            return propertyRepository.GetPropertyById(id);
        }
        public async Task<PropertyDto> CreateProperty(AddPropertyDto addPropertyDto)
        {
            var property = mapper.Map<Property>(addPropertyDto);
            property = await propertyRepository.CreateProperty(property);
            // map again to propertydto
            var propertyDto = mapper.Map<PropertyDto>(property);
            return propertyDto;
        }
        public async Task<PropertyDto> UpdateProperty(int id, UpdatePropertyDto updatePropertyDto)
        {
            var prop = mapper.Map<Property>(updatePropertyDto);
            prop = await propertyRepository.UpdateProperty(id, prop);
            // map again to propertydto
            var propertyDto = mapper.Map<PropertyDto>(prop);
            return propertyDto;
        }
        public async Task<Property> DeleteProperty(int id)
        {
           var deletedProperty = await propertyRepository.DeleteProperty(id);
           return deletedProperty;

        }

        
    }
}
