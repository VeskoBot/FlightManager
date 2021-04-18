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
        [MaxLength(20, ErrorMessage = "Name can be at most 20 characters long")]
        [MinLength(3, ErrorMessage = "Name cannot be less than 3 characters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must only contain letters")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Surename")]
        [MaxLength(20, ErrorMessage = "Surename can be at most 20 characters long")]
        [MinLength(3, ErrorMessage = "Surename cannot be less than 3 characters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must only contain letters")]
        public string Surename { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Family name")]
        [MaxLength(20, ErrorMessage = "Family name can be at most 20 characters long")]
        [MinLength(3, ErrorMessage = "Family name cannot be less than 3 characters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must only contain letters")]
        public string LastName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "EGN")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        [MaxLength(10, ErrorMessage = "EGN can be at most 10 characters long")]
        [MinLength(10, ErrorMessage = "EGN name cannot be less than 10 characters long")]
        public string EGN { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Phone number")]
        [MaxLength(10, ErrorMessage = "Phone number can be at most 10 characters long")]
        [MinLength(10, ErrorMessage = "Phone number name cannot be less than 10 characters long")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Nationality")]
        [MaxLength(20, ErrorMessage = "Nationallity can be at most 20 characters long")]
        [MinLength(3, ErrorMessage = "Nationallity cannot be less than 3 characters long")]
        public string Nationality { get; set; }
        [EmailAddress]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(50, ErrorMessage = "E-mail can be at most 50 characters long")]
        [Display(Name = "E-mail addresss")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Ticket Type")]
        public int TicketType { get; set; }
        public Flights Flight { get; set; }
        [Required]
        public int FlightId { get; set; }
    }
}
