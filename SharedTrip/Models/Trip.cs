using System;
using static SharedTrip.Data.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Globalization;

namespace SharedTrip.Models
{
    public class Trip
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string StartPoint { get; init; }
        [Required]
        public string EndPoint { get; init; }
        [Required]
        public DateTime DepartureTime { get; set; } = DateTime.UtcNow;

        [MaxLength(MaxSeatValue)]
        [MinLength(MinSeatValue)]
        [Required]
        public int Seats { get; init; }
        [MaxLength(DescriptionMaxLength)]
        [Required]
        public string Description { get; init; }

        public string ImagePath { get; init; }

        public IEnumerable<UserTrip> UserTrips { get; init; } = new List<UserTrip>();
    }
}
