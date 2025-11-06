using RentEase.API.Models.Domain;
using RentEase.API.Models.DTOs;

namespace RentEase.API.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<List<Property>> GetAllProperties();
        Task<Property> GetPropertyById(int id);
        Task<Property> CreateProperty(AddPropertyDto addPropertyDto);
        Task<Property> UpdateProperty(int id , UpdatePropertyDto updatePropertyDto);
        Task<Property> DeleteProperty(int id);
    }
}
