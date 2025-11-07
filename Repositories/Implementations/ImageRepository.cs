using RentEase.API.Data;
using RentEase.API.Models.Domain;
using RentEase.API.Repositories.Interfaces;

namespace RentEase.API.Repositories.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly RentEaseDbContext dbContext;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IHttpContextAccessor contextAccessor;

        public ImageRepository(RentEaseDbContext dbContext, IWebHostEnvironment hostEnvironment,
            IHttpContextAccessor contextAccessor)
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
            this.contextAccessor = contextAccessor;
        }
        public async Task<Image> UploadImage(Image image)
        {
            var localFilePath = Path.Combine(hostEnvironment.ContentRootPath, "Images",
                $"{ image.FileName}{ image.FileExtension}");

            // upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{contextAccessor.HttpContext.Request.Scheme}://{contextAccessor.HttpContext.Request.Host}{contextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
