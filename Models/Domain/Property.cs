using Microsoft.EntityFrameworkCore;
using RentEase.API.Models.Enums;

namespace RentEase.API.Models.Domain
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        // Precision attribute for tell EF to accept decimal value of 12 digits, 2 deciaml places
        [Precision(12,2)]
        public decimal RentAmount { get; set; }
        public string Location { get; set; }
        public PropertyStatus Status { get; set; } = PropertyStatus.Available;
        public DateTime PostedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // navigation property for images
        public ICollection<Image> PropertyImages { get; set; } = new List<Image>();

    }
}
