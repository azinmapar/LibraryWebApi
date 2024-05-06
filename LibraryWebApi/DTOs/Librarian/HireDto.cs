using LibraryWebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApi.DTOs.Librarian
{
    public class HireDto
    {


        [Required]
        [MinLength(5, ErrorMessage = "Username must be 5 characters")]
        [MaxLength(25, ErrorMessage = "Username cannot be over 25 characters")]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        [MinLength(11, ErrorMessage = "Phone Number must be 11 characters")]
        [MaxLength(11, ErrorMessage = "Phone Number cannot be 11 characters")]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "National Id must be 10 characters")]
        [MaxLength(10, ErrorMessage = "National Id cannot be 10 characters")]
        public string? NationalId { get; set; } = string.Empty;

        [Required]
        public string? Address { get; set; } = string.Empty;

        //hired date
        public DateTime StartWork { get; set; } = DateTime.Now;

        [Required]
        public bool IsManager { get; set; } = false;


    }
}
