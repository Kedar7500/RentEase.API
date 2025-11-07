using RentEase.API.Models.Domain;

namespace RentEase.API.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> UploadImage(Image image);
    }
}
