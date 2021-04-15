using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace FlightManager.Models
{
    public class User
    {
        [Required]
        [Key]
        public int UserId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [NotMapped]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "E-mail addresss")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Family name")]
        public string LastName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "EGN")]
        public string EGN { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "System role")]
        public int Role { get; set; }
    }
}
