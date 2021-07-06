using System;
using static SharedTrip.Data.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SharedTrip.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(UserMinUsername)]
        [MaxLength(UserMaxUsername)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<UserTrip> UserTrips { get; set; } = new List<UserTrip>();
    }
}
