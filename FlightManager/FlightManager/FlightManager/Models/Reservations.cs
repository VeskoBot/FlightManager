using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FlightManager.Data;

namespace FlightManager.Models
{
    public class Reservations
    {
        [Required]
        [Key]
        public int ReservationId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Surename")]
        public string Surename { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Family name")]
        public string LastName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "EGN")]
        public string EGN { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
        [Required]
        [Display(Name = "Ticket Type")]
        public int TicketType { get; set; }
        public Flights Flight { get; set; }
    }
}
