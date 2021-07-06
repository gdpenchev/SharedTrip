using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Forms.Trip
{
    public class DetailsTripFormModel
    {
        public string TripId { get; init; }
        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; init; }

        public int Seats { get; set; }

        public string Description { get; set; }
    }
}
