using LibraryWebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApi.DTOs.Member
{
    public class RegisterMemberDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MinLength(10, ErrorMessage = "National Id must be 10 characters")]
        [MaxLength(10, ErrorMessage = "National Id cannot be 10 characters")]
        [RegularExpression("(^[0-9]*)(^[12].*)", ErrorMessage = "National ID must be numeric")]
        public string NationalId { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Phone]
        [MinLength(11, ErrorMessage = "Phone Number must be 11 characters")]
        [MaxLength(11, ErrorMessage = "Phone Number cannot be 11 characters")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        //the day this member signed up in the library
        public DateTime RegisterDate { get; set; } = DateTime.Now;


    }
}
