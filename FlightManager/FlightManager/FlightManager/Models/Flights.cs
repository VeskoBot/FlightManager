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
        [MaxLength(30, ErrorMessage = "Location name can be at most 30 characters long")]
        [MinLength(3, ErrorMessage = "Location name cannot be less than 3 characters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must only contain letters")]
        public string DepartureLoaction { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        [Display(Name = "Place of arrival")]
        [MaxLength(30, ErrorMessage = "Location name can be at most 30 characters long")]
        [MinLength(3, ErrorMessage = "Location name cannot be less than 3 characters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must only contain letters")]
        public string ArrivalLocation { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Time of Departure")]
        public DateTime DepartureTime { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Time of Arrival")]
        public DateTime ArrivalTime { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Airplane model")]
        [MaxLength(20, ErrorMessage = "Airplane model can be at most 20 characters long")]
        [MinLength(4, ErrorMessage = "Airplane model cannot be less than 4 characters long")]
        public string AirplaneType { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Unique plane number")]
        [MaxLength(10, ErrorMessage = "Unique plane number can be at most 10 characters long")]
        [MinLength(4, ErrorMessage = "Unique plane number cannot be less than 4 characters long")]
        public string AirplaneCode { get; set; }
        [Required]
        [Display(Name = "Vacant seats in economy")]
        [Range(0, 100, ErrorMessage = "The seats cannot be more than 100 or less than 0")]
        public int VacantSpots { get; set; }
        [Required]
        [Display(Name = "Vacant seats in bussiness class")]
        [Range(0, 100, ErrorMessage = "The seats cannot be more than 100 or less than 0")]
        public int VacantSpotsBussiness { get; set; }
        public List<Reservations> ReservationsList { get; set; }

    }
}
