using System.ComponentModel.DataAnnotations;

namespace LibraryWebApi.DTOs.Librarian
{
    public class LoginDto
    {

        [Required]
        [MinLength(5, ErrorMessage = "Username must be 5 characters")]
        [MaxLength(25, ErrorMessage = "Username cannot be over 25 characters")]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }


    }
}
