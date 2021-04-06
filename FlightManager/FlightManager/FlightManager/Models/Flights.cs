using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class Flights
    {
        [Required]
        [Key]
        public int FlightId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        [Display(Name = "Place of departure")]
        public string DepartureLoaction { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        [Display(Name = "Place of Arrival")]
        public string ArrivalLocation { get; set; }
        [Required]
        [Column(TypeName = "date")]
        [Display(Name = "Time of Departure")]
        public DateTime DepartureTime { get; set; }
        [Required]
        [Column(TypeName = "date")]
        [Display(Name = "Time of Arrival")]
        public DateTime ArrivalTime { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Airplane model")]
        public string AirplaneType { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Unique plane number")]
        public string AirplaneCode { get; set; }
        [Required]
        [Display(Name = "Total amount of vacant seats")]
        public int VacantSpots { get; set; }
        [Required]
        [Display(Name = "Vacant seats in bussiness class")]
        public int VacantSpotsBussiness { get; set; }
        public List<Reservations> ReservationsList { get; set; }

    }
}
