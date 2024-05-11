using System.ComponentModel.DataAnnotations;

namespace LibraryWebApi.DTOs.Librarian
{
    public class UpdateLibrarianInfoDto
    {


        
        [MinLength(5, ErrorMessage = "Username must be 5 characters")]
        [MaxLength(25, ErrorMessage = "Username cannot be over 25 characters")]
        public string? Username { get; set; }

        public string? Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        [MinLength(11, ErrorMessage = "Phone Number must be 11 characters")]
        [MaxLength(11, ErrorMessage = "Phone Number cannot be 11 characters")]
        public string? PhoneNumber { get; set; }

        public string? FullName { get; set; }

        public string? Address { get; set; }

        public bool? IsManager { get; set; } = false;


    }
}
