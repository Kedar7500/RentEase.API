using AutoMapper;
using Microsoft.Extensions.Hosting;
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
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IWebHostEnvironment hostEnvironment;

        public PropertyService(IPropertyInterface propertyRepository, IMapper mapper,
            IHttpContextAccessor contextAccessor, IWebHostEnvironment hostEnvironment)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task<List<PropertyDto>> GetAllProperties()
        {
            var propertyDomain = await propertyRepository.GetAllProperty();
            var propertyDto = mapper.Map<List<PropertyDto>>(propertyDomain);

            return propertyDto;
            
            
        }
        public Task<Property> GetPropertyById(int id)
        {
            return propertyRepository.GetPropertyById(id);
        }
        public async Task<PropertyDto> CreateProperty(AddPropertyDto addPropertyDto)
        {
            var property = mapper.Map<Property>(addPropertyDto);

            var images = new List<Image>();

            if (addPropertyDto.Images != null && addPropertyDto.Images.Count > 0) 
            {
              
                foreach (var file in addPropertyDto.Images)
                {
                    var extension = Path.GetExtension(file.FileName.ToLowerInvariant());

                    var urlFilePath = $"{contextAccessor.HttpContext.Request.Scheme}://{contextAccessor.HttpContext.Request.Host}{contextAccessor.HttpContext.Request.PathBase}/Images/{file.FileName}";

                    var image = new Image
                    { 
                        Description = addPropertyDto.Description,
                        File = file,
                        FileExtension = extension,
                        FileName = file.FileName,
                        FileSizeInBytes = file.Length,
                        FilePath = urlFilePath
                    };

                    var localFilePath = Path.Combine(hostEnvironment.ContentRootPath, "Images",
                     $"{file.FileName}{extension}");

                    // upload image to local path
                    using var stream = new FileStream(localFilePath, FileMode.Create);
                    await image.File.CopyToAsync(stream);

                    images.Add(image);
                }

                property.PropertyImages = images;
            }

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
