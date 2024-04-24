using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApi.Models
{

    [Table("Lends")]
    public class Lend
    {

        public int Id { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; } = new Book();

        public int MemberId { get; set; }

        public Member Member { get; set; } = new Member();

        public int LibrarianId { get; set; }

        public Librarian Librarian { get; set; } = new Librarian();

        public DateTime BorrowedDate { get; set; } = DateTime.Now;

        public DateTime DueDate {  get; set; } = DateTime.Now;

        public DateTime? ReturnedDate {  get; set; }

    }
}
