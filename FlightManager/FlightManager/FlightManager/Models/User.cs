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
        [MaxLength(20, ErrorMessage = "Username can be at most 20 characters long")]
        [MinLength(3, ErrorMessage = "Username cannot be less than 3 characters long")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        [Column(TypeName = "nvarchar(100)")]
        [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters long")]
        public string Password { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [NotMapped]
        [Display(Name = "Confirm password")]
        [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters long")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(50, ErrorMessage = "E-mail can be at most 50 characters long")]
        [Display(Name = "E-mail addresss")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "First name")]
        [MaxLength(20, ErrorMessage = "Name can be at most 20 characters long")]
        [MinLength(3, ErrorMessage = "Name cannot be less than 3 characters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must only contain letters")]
        public string FirstName { get; set; }
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
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Address")]
        [MaxLength(50, ErrorMessage = "Address can be at most 50 characters long")]
        [MinLength(3, ErrorMessage = "Address cannot be less than 3 characters long")]
        public string Address { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Phone number")]
        [MaxLength(10, ErrorMessage = "Phone number can be at most 10 characters long")]
        [MinLength(10, ErrorMessage = "Phone number name cannot be less than 10 characters long")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "System role")]
        public int Role { get; set; }
    }
}
