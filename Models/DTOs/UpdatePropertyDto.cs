using Microsoft.EntityFrameworkCore;
using RentEase.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentEase.API.Models.DTOs
{
    public class UpdatePropertyDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Title must be length of 50 characters")]
        public string Title { get; set; }

        [MaxLength(100, ErrorMessage = "Description is upto 100 characters only")]
        public string? Description { get; set; }

        [Required]
        [Precision(12, 2)]
        public decimal RentAmount { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Location must be length of 50 characters")]
        public string Location { get; set; }

        [Required]
        public PropertyStatus Status { get; set; } = PropertyStatus.Available; 
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
