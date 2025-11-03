using RentEase.API.Models.Enums;

namespace RentEase.API.Models.DTOs
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal RentAmount { get; set; }
        public string Location { get; set; }
        public PropertyStatus Status { get; set; } = PropertyStatus.Available;
    }
}
