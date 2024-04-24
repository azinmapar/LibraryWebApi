using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApi.Models
{

    [Table("Books")]
    public class Book
    {

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public DateTime Released { get; set; } = DateTime.MinValue;

        public string Isbn { get; set; } = string.Empty;

        public int Pages { get; set; }

        public string Publisher { get; set; } = string.Empty;

        public string Language {  get; set; } = string.Empty;

        //info on them being lent
        public List<Lend> Lends { get; set; } = [];

        //librarian that registered them
        public int RegisterLibrarianId { get; set; }

        public Librarian RegisterLibrarian { get; set; } = new Librarian();

    }
}
