using System.ComponentModel.DataAnnotations;

namespace LibraryWebApi.DTOs.Librarian
{
    public class NewLibrarianDto
    {

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? FullName { get; set; }

        public string? NationalId { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

        public DateTime StartWork { get; set; } = DateTime.Now;

        public bool IsManager { get; set; } = false;


    }
}
