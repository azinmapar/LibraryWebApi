//using LibraryWebApi.Models;
//using System.ComponentModel.DataAnnotations;

//namespace LibraryWebApi.DTOs.Librarian
//{
//    public class HireLibrarianDto
//    {

//        // username, password, email, phone number is in there

//        [Required]
//        public string? Username { get; set; }

//        [Required]
//        public string? Password { get; set; }

//        [Required]
//        [EmailAddress]
//        public string? Email { get; set; }

//        [Required]
//        public string? FullName { get; set; }

//        [Required]
//        public string? NationalId { get; set; } = string.Empty;

//        [Required]
//        public string? Address { get; set; } = string.Empty;

        
//        public DateTime StartWork { get; set; } = DateTime.Now;

//        public int? RegisterManagerId { get; set; }

//        public Librarian? RegisterManager { get; set; }

//        //if they are manager => librarians they registered
//        public List<Librarian> RegisteredLibrarians { get; set; } = [];

//        //info on their lent books
//        public List<Lend> Lends { get; set; } = [];

//    }
//}
