using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApi.Models
{

    [Table("BookEditions")]
    public class BookEdition
    {

        public int EditionId { get; set; }

        public string Isbn { get; set; } = string.Empty;

        public int Pages { get; set; }

        public string Publisher { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;

        public bool Hardback { get; set; } = false;

        public int AvailableCopy {  get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; } = new Book();

        //info on them being lent
        public List<Lend> Lends { get; set; } = [];

        public string RegisterLibrarianId { get; set; } = string.Empty;

        public Librarian RegisterLibrarian { get; set; } = new Librarian();

    }
}
