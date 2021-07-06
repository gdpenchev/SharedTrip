using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Forms.Trip;
using SharedTrip.Models;
using SharedTrip.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IValidator validator;

        public TripsController(ApplicationDbContext data, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }
        [Authorize]
        public HttpResponse Add() => View();
        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddTripFormModel model)
        {
            var modelErrors = this.validator.ValidateTrip(model);

            if (modelErrors.Any())
            {
                return Redirect("/Trips/Add");
            }

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = model.DepartureTime,
                Seats = model.Seats,
                ImagePath = model.ImagePath,
                Description = model.Description
            };
            var userTrip = new UserTrip
            {
                UserId = this.User.Id,
                Trip = trip
            };

            this.data.UserTrips.Add(userTrip);
            this.data.Trips.Add(trip);
            this.data.SaveChanges();



            return Redirect("/Trips/All");
        }
        [Authorize]
        public HttpResponse All()
        {
            var allTrips = this.data.Trips
                .Select(t => new TripAllListingViewFormModel
                {
                    TripId = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime,
                    Seats = t.Seats
                })
                .ToList();

            return View(allTrips);
        }
        [Authorize]
        public HttpResponse Details(string tripId)
        {
            if (tripId == null)
            {
                return BadRequest();
            }
            var currentTrip = this.data.Trips
                .Where(t => t.Id == tripId)
                .Select(t => new DetailsTripFormModel
                {
                    TripId = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime,
                    Seats = t.Seats,
                    Description = t.Description
                })
                .FirstOrDefault();

            return View(currentTrip);
        }
        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            var currentTrip = this.data.Trips
                .Where(t => t.Id == tripId)
                .FirstOrDefault();

            var currentUser = this.data.Users
                .Where(u => u.Id == this.User.Id)
                .FirstOrDefault();

            var alreadyExist = this.data.UserTrips
                .Where(ut => ut.TripId == currentTrip.Id && ut.UserId == currentUser.Id).FirstOrDefault();

            if (alreadyExist != null)
            {
                return Error($"This user {currentUser.Username} is already listed for this trip");
            }

            var tripSeats = currentTrip.Seats;
            tripSeats--;
            var updatedTrip = new Trip
            {
                Id = currentTrip.Id,
                StartPoint = currentTrip.StartPoint,
                EndPoint = currentTrip.EndPoint,
                DepartureTime = currentTrip.DepartureTime,
                Seats = tripSeats,
                Description = currentTrip.Description,
                ImagePath = currentTrip.ImagePath
            };


            var userTrip = new UserTrip
            {
                UserId = currentUser.Id,
                TripId = currentTrip.Id
            };

            this.data.UserTrips.Add(userTrip);
            this.data.Entry(currentTrip).CurrentValues.SetValues(updatedTrip);
            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }


    }
}
