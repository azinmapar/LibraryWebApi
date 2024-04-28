using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApi.Models
{

    [Table("Lends")]
    public class Lend
    {

        public int LendId { get; set; }

        public int EditionId { get; set; }

        public BookEdition Edition { get; set; } = new BookEdition();

        public int MemberId { get; set; }

        public Member Member { get; set; } = new Member();

        public string LibrarianId { get; set; } = string.Empty;

        public Librarian Librarian { get; set; } = new Librarian();

        public DateTime BorrowedDate { get; set; } = DateTime.Now;

        public DateTime DueDate {  get; set; } = DateTime.Now.AddDays(14);

        public DateTime? ReturnedDate {  get; set; }

    }
}
