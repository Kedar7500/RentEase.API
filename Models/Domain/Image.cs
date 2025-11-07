using System.ComponentModel.DataAnnotations.Schema;

namespace RentEase.API.Models.Domain
{
    public class Image
    {
        public int Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? Description { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }

        // relationship to property
        public int PropertyId { get; set; }
        public Property? Property { get; set; }
    }
}
