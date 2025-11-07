using AutoMapper;
using RentEase.API.Models.Domain;
using RentEase.API.Models.DTOs;
using RentEase.API.Repositories.Interfaces;
using RentEase.API.Services.Interfaces;

namespace RentEase.API.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public ImageService(IMapper mapper, IImageRepository imageRepository)
        {
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }
        public Task<Image> UploadImage(ImageUploadRequestDto requestDto)
        {
            // convert the dto to domain object
            var imageDomain = new Image
            {
                FileName = requestDto.FileName,
                Description = requestDto.FileDescription,
                FileExtension = Path.GetExtension(requestDto.File.FileName),
                File = requestDto.File,
                FileSizeInBytes = requestDto.File.Length
            };
            var image = imageRepository.UploadImage(imageDomain);
            return image;
        }
    }
}
