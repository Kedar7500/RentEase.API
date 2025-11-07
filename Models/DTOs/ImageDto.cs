namespace RentEase.API.Models.DTOs
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }
}
