using Microsoft.AspNetCore.Identity;

namespace LibraryWebApi.Models
{
    public class Librarian : IdentityUser
    {

        // username, password, email, phone number is in there

        public string FullName { get; set; } = string.Empty;

        public string NationalId { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        //hired date
        public DateTime StartWork { get; set; } = DateTime.Now;

        //fired/resigned date
        public DateTime? EndWork { get; set; }

        //still working in this position (by Delete this will be false)
        public bool IsActive { get; set; } = true;

        //members they registered
        public List<Member> RegisteredMembers { get; set; } = [];

        //books they registered
        public List<BookEdition> RegisteredEditions { get; set; } = [];

        public string RegisterManagerId { get; set; } = string.Empty;

        public Librarian RegisterManager { get; set; } = new Librarian();

        //if they are manager => librarians they registered
        public List<Librarian> RegisteredLibrarians { get; set; } = [];

        //info on their lent books
        public List<Lend> Lends { get; set; } = [];

    }
}
