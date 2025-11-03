using RentEase.API.Models.Domain;

namespace RentEase.API.Repositories.Interfaces
{
    public interface IPropertyInterface
    {
        Task<List<Property>> GetAllProperty();
        Task<Property> GetPropertyById(int id);
        Task<Property> CreateProperty(Property property);
        Task<Property> UpdateProperty(int id, Property property);
        Task<Property> DeleteProperty(int id);
    }
}
