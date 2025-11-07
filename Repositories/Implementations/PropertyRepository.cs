using Microsoft.EntityFrameworkCore;
using RentEase.API.Data;
using RentEase.API.Models.Domain;
using RentEase.API.Repositories.Interfaces;

namespace RentEase.API.Repositories.Implementations
{
    public class PropertyRepository : IPropertyInterface
    {
        private readonly RentEaseDbContext dbContext;

        public PropertyRepository(RentEaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Property>> GetAllProperty()
        {
            return await dbContext.Properties.Include(p => p.PropertyImages).ToListAsync();
        }

        public async Task<Property> GetPropertyById(int id)
        {
            var property = await dbContext.Properties.FirstOrDefaultAsync(p => p.Id == id);
            return property;
        }

        public async Task<Property> CreateProperty(Property property)
        {
            await dbContext.Properties.AddAsync(property);
            await dbContext.SaveChangesAsync();

            if (property.PropertyImages != null && property.PropertyImages.Count > 0)
            {
                foreach (var image in property.PropertyImages)
                {
                    image.PropertyId = property.Id;
                }
                await dbContext.Images.AddRangeAsync(property.PropertyImages);
                await dbContext.SaveChangesAsync();
            }

           // await dbContext.SaveChangesAsync();
            return property;
        }

        public async Task<Property> UpdateProperty(int id, Property property)
        {
            var existingProperty = dbContext.Properties.FirstOrDefault(p => p.Id == id);

            if (existingProperty != null)
            {
                existingProperty.Title = property.Title;
                existingProperty.Description = property.Description;
                existingProperty.Location = property.Location;
                existingProperty.Status = property.Status;
                existingProperty.RentAmount = property.RentAmount;
                existingProperty.UpdatedAt = property.UpdatedAt;
            }

            dbContext.Properties.Update(existingProperty);
            await dbContext.SaveChangesAsync();

            return existingProperty;
        }

        public async Task<Property> DeleteProperty(int id)
        {
            var propertyToRemove = dbContext.Properties.FirstOrDefault(x => x.Id == id);
            dbContext.Properties.Remove(propertyToRemove);
            await dbContext.SaveChangesAsync();

            return propertyToRemove;
        }

        
    }
}
