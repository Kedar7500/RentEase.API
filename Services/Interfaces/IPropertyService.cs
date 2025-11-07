using RentEase.API.Models.Domain;
using RentEase.API.Models.DTOs;

namespace RentEase.API.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> GetAllProperties();
        Task<Property> GetPropertyById(int id);
        Task<PropertyDto> CreateProperty(AddPropertyDto addPropertyDto);
        Task<PropertyDto> UpdateProperty(int id , UpdatePropertyDto updatePropertyDto);
        Task<Property> DeleteProperty(int id);
    }
}
