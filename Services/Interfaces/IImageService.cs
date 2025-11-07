using RentEase.API.Models.Domain;
using RentEase.API.Models.DTOs;

namespace RentEase.API.Services.Interfaces
{
    public interface IImageService
    {
        Task<Image> UploadImage(ImageUploadRequestDto requestDto);
    }
}
