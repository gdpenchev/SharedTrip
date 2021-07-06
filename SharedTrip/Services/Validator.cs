using SharedTrip.Forms.Trip;
using SharedTrip.Forms.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedTrip.Data.DataConstants;

namespace SharedTrip.Services
{
    public class Validator : IValidator
    {
        

        public ICollection<string> ValidateUser(RegisterUserFormModel user)
        {
            var errors = new List<string>();

            if (user.Username == null || user.Username.Length < UserMinUsername || user.Username.Length > UserMaxUsername)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {UserMinUsername} and {UserMaxUsername} characters long.");
            }

            if (user.Password == null || user.Password.Length < PasswordMinLength || user.Password.Length > PasswordMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {PasswordMaxLength} characters long.");
            }

            if (user.Password != null && user.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (user.Password != user.ConfirmPassword)
            {
                errors.Add("Password and its confirmation are different.");
            }

            return errors;
        }
        public ICollection<string> ValidateTrip(AddTripFormModel trip)
        {
            var errors = new List<string>();

            if (trip.StartPoint == null || trip.EndPoint == null)
            {
                errors.Add($"Starting point and End point cannot be null.");
            }
            if (trip.Seats < MinSeatValue || trip.Seats > MaxSeatValue)
            {
                errors.Add($"Seats must be between {MinSeatValue} and {MaxSeatValue}.");
            }

            return errors;
        }
    }
}
